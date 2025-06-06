namespace Lab_1;

public class Controller
{
    private readonly Thread _thread; // Поток контроллера
    private bool isStopped = false; // Флаг для остановки работы контроллера

    // Буфер, с которым работает контроллер
    public Buffer Buffer { get; private set; }

    private readonly Writer _writer; // Объект писателя
    private readonly List<Reader> _readers = []; // Список объектов читателей

    // Событие, возникающее перед записью данных в буфер со стороны писателя
    public event Action<int>? WriterWaiting;

    // Событие, возникающее после успешной записи данных в буфер со стороны писателя
    public event Action<int>? WriterDone;

    // Событие, возникающее при удалении читателя
    public event Action<int>? ReaderDeleted;

    // Событие, возникающее после успешного чтения данных читателем
    public event Action<Reader, int>? ReaderDone;

    public Controller()
    {
        // Инициализация потока контроллера
        _thread = new(Work);

        Buffer = new(10);
        _writer = new(Buffer);

        // Инициализация объекта писателя и его событий
        _writer.Waiting += data =>
        {
            WriterWaiting?.Invoke(data);
        };

        _writer.Done += data =>
        {
            WriterDone?.Invoke(data);
        };
    }

    // Метод для запуска работы контроллера
    public void Start()
    {
        _thread.Start();
        _writer.Run();
    }

    // Метод для остановки работы контроллера
    public void Stop()
    {
        isStopped = true;

        _writer.Stop(); // Остановка работы писателя

        // Остановка работы всех читателей
        _readers.ForEach(reader => reader.Stop());
    }

    // Метод для создания нового читателя
    private void NewReader()
    {
        // Создание нового объекта читателя (закидываем в него наш общий буфер)
        Reader reader = new(Buffer);

        // Обработчик события чтения данных читателем
        reader.Read += data =>
        {
            ReaderDone?.Invoke(reader, data);
        };

        // Обработчик события удаления читателя
        reader.Deleted += () =>
        {
            ReaderDeleted?.Invoke(reader.Id);
        };

        // Добавление читателя в список читателей контроллера
        _readers.Add(reader);

        // Если работа контроллера остановлена, то выход из метода
        if (isStopped)
        {
            return;
        }

        // Запуск работы читателя
        reader.Start();
    }

    // Метод, выполняемый в потоке контроллера
    private void Work()
    {
        // Цикл работы контроллера
        // Если isStopped == true, то поток завершит свою работу.
        // Завершает свою работу в методе Stop().
        while (isStopped != true)
        {
            // Задержка перед созданием нового читателя
            Thread.Sleep(new Random().Next(1000, 2000));

            // Создание нового читателя
            NewReader();
        }
    }
}
