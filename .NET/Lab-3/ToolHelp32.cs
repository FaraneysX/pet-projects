using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Lab_3;

public class ToolHelp32
{
    // Определение флагов для параметров снимка.
    [Flags]
    internal enum SnapshotFlags : uint
    {
        HeapList = 0x00000001, // Включение в снимок список куч заданного процесса Win32.
        Process = 0x00000002, // Включение в снимок список процессов Win32.
        Thread = 0x00000004, // Включение в снимок список потоков Win32.
        Module = 0x00000008, // Включение в снимок список модулей заданного процесса Win32.
        Module32 = 0x000000010,
        Inherit = 0x80000000,
        All = 0x0000001F,
        NoHeaps = 0x40000000
    }

    // Определение структуры для записи о процессе в снимке.
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct PROCESSENTRY32
    {
        public uint dwSize; // Размер записи.
        public uint cntUsage; // Счетчик ссылок процесса.
        public uint th32ProcessID; // Идентификационный номер процесса.
        public IntPtr th32DefaultHeapID;
        public uint th32ModuleID; // Идентифицирует модуль, связанный с процессом.
        public uint cntThreads; // Количество потоков в данном процессе.
        public uint th32ParentProcessID; // Идентификатор родительского процесса.
        public int pcPriClassBase; // Базовый приоритет процесса.
        public uint dwFlags; // Зарезервировано.

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szExeFile;
    }

    // Определение структуры для записи о модуле в снимке.
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct MODULEENTRY32
    {
        public uint dwSize;
        public uint th32ModuleID;
        public uint th32ProcessID;
        public uint GlblcntUsage;
        public uint ProccntUsage;
        public IntPtr modBaseAddr;
        public uint modBaseSize; // Размер модуля.
        public IntPtr hModule;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string szModule;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szExePath;
    }

    // Импорт необходимых функций из kernel32.dll.
    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
    static public extern bool Module32First([In] IntPtr hSnapshot, ref MODULEENTRY32 lpme);

    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
    static public extern bool Module32Next([In] IntPtr hSnapshot, ref MODULEENTRY32 lpme);

    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
    static extern bool Process32First([In] IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
    static extern bool Process32Next([In] IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern IntPtr CreateToolhelp32Snapshot([In] UInt32 dwFlags, [In] uint th32ProcessID);

    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern bool CloseToolhelp32Snapshot([In] IntPtr hSnapshot);

    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern bool CloseHandle([In] IntPtr hObject);

    public List<PROCESSENTRY32> GetProcessesWithMaxModuleSize()
    {
        // Инициализация переменных для отслеживания максимального размера модуля и указателя на снимок процессов.
        int maxModuleSize = 0;
        IntPtr handleToSnapshot = IntPtr.Zero;

        // Создание списка для хранения информации о процессах.
        List<PROCESSENTRY32> processes = [];

        try
        {
            // Создание структуры для информации о текущем процессе.
            PROCESSENTRY32 process = new()
            {
                dwSize = (uint)Marshal.SizeOf(typeof(PROCESSENTRY32))
            };

            // Создание снимка текущих процессов (0 - снимок всех процессов в системе).
            handleToSnapshot = CreateToolhelp32Snapshot((uint)SnapshotFlags.Process, 0);

            // Проверка успешности создания снимка.
            if (handleToSnapshot == IntPtr.Zero || handleToSnapshot == -1)
            {
                throw new ApplicationException("Failed to create snapshot");
            }

            // Получение информации о первом процессе в снимке.
            if (Process32First(handleToSnapshot, ref process))
            {
                do
                {
                    // Получение размера модуля для текущего процесса.
                    int moduleSize = TotalModuleSize(process.th32ProcessID);

                    if (moduleSize > maxModuleSize)
                    {
                        maxModuleSize = moduleSize;
                        processes.Clear();
                        processes.Add(process);
                    }
                    else if (moduleSize == maxModuleSize)
                    {
                        processes.Add(process);
                    }
                } while (Process32Next(handleToSnapshot, ref process));
            }
            else
            {
                int error = Marshal.GetLastWin32Error();

                throw new ApplicationException($"Failed with win32 error code {error}: {new Win32Exception(error).Message}");
            }
        }
        finally
        {
            // Закрытие дескриптора снимка процессов.
            CloseHandle(handleToSnapshot);
        }

        return processes;
    }

    public static int TotalModuleSize(uint processId)
    {
        // Инициализация переменных для отслеживания общего размера модулей и указателя на снимок модулей.
        IntPtr handleToSnapshot = IntPtr.Zero;
        int totalSize = 0;

        try
        {
            // Создание структуры для информации о текущем модуле.
            MODULEENTRY32 module = new()
            {
                dwSize = (uint)Marshal.SizeOf(typeof(MODULEENTRY32))
            };

            // Создание снимка модулей для указанного процесса.
            handleToSnapshot = CreateToolhelp32Snapshot((uint)SnapshotFlags.Module, processId);

            // Проверка успешности создания снимка модулей.
            if (handleToSnapshot == IntPtr.Zero || handleToSnapshot == -1)
            {
                return -1;
            }

            // Получение информации о первом модуле в снимке.
            if (Module32First(handleToSnapshot, ref module))
            {
                do
                {
                    totalSize += (int)module.modBaseSize;
                } while (Module32Next(handleToSnapshot, ref module));
            }
            else
            {
                int error = Marshal.GetLastWin32Error();

                throw new ApplicationException($"Failed with win32 error code {error}: {new Win32Exception(error).Message}");
            }
        }
        finally
        {
            // Закрытие дескриптора снимка модулей.
            CloseHandle(handleToSnapshot);
        }

        return totalSize;
    }
}
