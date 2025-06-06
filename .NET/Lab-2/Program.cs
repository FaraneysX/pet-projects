// Лабораторная работа #2.
// Задание 6.
//
// Заданы два каталога.
// Проверить, есть ли во втором файлы, созданные раньше,
// чем любой из первого каталога.

namespace Lab_2;

class Program
{
    // Метод для получения времени создания самого раннего файла в указанном каталоге.
    static DateTime GetEarlierFileCreationTime(string? directory)
    {
        // Инициализация переменной, представляющей самую минимальную дату (максимальное значение DateTime).
        DateTime earliestDate = DateTime.MaxValue;
        string fileName = string.Empty;

        // Используем using для автоматического закрытия дескриптора поиска.
        using SafeFindHandle findHandle = RsdnDirectory.FindFirstFile(directory + "\\*", out RsdnDirectory.WIN32_FIND_DATA data);

        // Проверка, что дескриптор поиска был успешно создан.
        if (findHandle != null)
        {
            // Цикл по файлам в каталоге.
            do
            {
                // Проверка, что текущий элемент не является директорией.
                if ((data.dwFileAttributes & FileAttributes.Directory) != FileAttributes.Directory)
                {
                    // Конвертация файла в DateTime из структуры WIN32_FIND_DATA.
                    // Создание 64-битного значения из старшей и младшей частей, сохраненных в структуре FILETIME,
                    // представляющей времена файлов в интервалах по 100 наносекунд с 1 января 1601 года (время в формате UTC).
                    // Это 64-битное значение затем может быть преобразовано в объект DateTime, представляющий время создания файла.
                    DateTime creationTime = DateTime.FromFileTimeUtc((((long)data.ftCreationTime.dwHighDateTime) << 32) | (uint)data.ftCreationTime.dwLowDateTime);

                    if (creationTime < earliestDate)
                    {
                        earliestDate = creationTime;
                        fileName = data.cFileName;
                    }
                }
            } while (RsdnDirectory.FindNextFile(findHandle, out data));
        }

        Console.WriteLine($"Файл, который был раньше всего создан в директории {directory}: {fileName} ({earliestDate})");

        return earliestDate;
    }

    // Метод для получения времени создания самого позднего файла в указанном каталоге.
    static DateTime GetLastFileCreationTime(string? directory)
    {
        // Инициализация переменной, представляющей самую позднюю дату (минимальное значение DateTime).
        DateTime latestDate = DateTime.MinValue;

        // Используем using для автоматического закрытия дескриптора поиска.
        using SafeFindHandle findHandle = RsdnDirectory.FindFirstFile(directory + "\\*", out RsdnDirectory.WIN32_FIND_DATA data);

        // Проверка, что дескриптор поиска был успешно создан.
        if (findHandle != null)
        {
            // Цикл по файлам в каталоге.
            do
            {
                // Проверка, что текущий элемент не является директорией.
                if ((data.dwFileAttributes & FileAttributes.Directory) != FileAttributes.Directory)
                {
                    // Конвертация файла в DateTime из структуры WIN32_FIND_DATA.
                    DateTime creationTime = DateTime.FromFileTimeUtc((((long)data.ftCreationTime.dwHighDateTime) << 32) | (uint)data.ftCreationTime.dwLowDateTime);

                    if (creationTime > latestDate)
                    {
                        latestDate = creationTime;
                    }
                }
            } while (RsdnDirectory.FindNextFile(findHandle, out data));
        }

        return latestDate;
    }

    static void Main()
    {
        Console.Write("Введите путь к 1-му каталогу: ");
        string? firstDirectory = Console.ReadLine();

        Console.Write("Введите путь ко 2-му каталогу: ");
        string? secondDirectory = Console.ReadLine();

        DateTime latestFirstDirectory = default;
        DateTime earliestSecondDirectory = default;

        // Создание и запуск двух потоков для параллельного получения времени создания файлов.
        Thread firstThread = new(() =>
        {
            try
            {
                latestFirstDirectory = GetLastFileCreationTime(firstDirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении каталога: {ex.Message}");
            }
        });

        Thread secondThread = new(() =>
        {
            try
            {
                earliestSecondDirectory = GetEarlierFileCreationTime(secondDirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении каталога: {ex.Message}");
            }
        });

        firstThread.Start();
        secondThread.Start();

        // Ожидание завершения выполнения потоков.
        firstThread.Join();
        secondThread.Join();

        if (earliestSecondDirectory < latestFirstDirectory)
        {
            Console.WriteLine($"\nВо втором каталоге ({secondDirectory}) есть файл, созданный раньше любого файла из первого каталога ({firstDirectory}).");
        }
        else
        {
            Console.WriteLine($"\nВо втором каталоге ({secondDirectory}) нет файлов, созданных раньше, чем файлы в первом каталоге ({firstDirectory}).");
        }
    }
}