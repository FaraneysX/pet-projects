using System.Runtime.InteropServices;

using Microsoft.Win32.SafeHandles;

using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

namespace Lab_2;

// Класс RsdnDirectory содержит определения для работы с функциями Win32 для работы с файловой системой.
public class RsdnDirectory
{
    // Константа MAX_PATH определяет максимальную длину пути в символах.
    private const int MAX_PATH = 260;

    // Структура WIN32_FIND_DATA представляет данные о найденном файле.
    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    [BestFitMapping(false)]
    public struct WIN32_FIND_DATA
    {
        public FileAttributes dwFileAttributes;
        public FILETIME ftCreationTime;
        public FILETIME ftLastAccessTime;
        public FILETIME ftLastWriteTime;
        public int nFileSizeHigh;
        public int nFileSizeLow;
        public int dwReserved0;
        public int dwReserved1;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
        public string cFileName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
        public string cAlternate;
    }

    // Функция FindFirstFile используется для поиска файлов в каталоге.
    [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern SafeFindHandle FindFirstFile(string lpFileName, out WIN32_FIND_DATA lpFindData);

    // Функция FindNextFile используется для продолжения поиска файлов в каталоге после вызова FindFirstFile.
    [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool FindNextFile(SafeFindHandle hFindFile, out WIN32_FIND_DATA lpFindFileData);

    // Функция FindClose закрывает дескриптор поиска, который был открыт с помощью FindFirstFile.
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool FindClose(IntPtr hFindFile);
}

// Класс SafeFindHandle представляет безопасное обертывание для дескриптора поиска, который затем используется в SafeHandle.
public class SafeFindHandle : SafeHandleMinusOneIsInvalid
{
    public SafeFindHandle() : base(true) { }

    // Метод ReleaseHandle вызывается при освобождении дескриптора.
    protected override bool ReleaseHandle()
    {
        // Вызываем FindClose для закрытия дескриптора поиска.
        return RsdnDirectory.FindClose(handle);
    }
}