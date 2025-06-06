namespace Lab_1;

public class Reader
{
    private static int _counter = 0; // Статический счетчик для назначения идентификаторов читателям

    private readonly Thread _thread; // Поток, в котором выполняется работа читателя
    private readonly Buffer _buffer; // Буфер, из которого читатель считывает данные
    private bool isStopped = false; // Флаг для остановки работы читателя

    // Идентификатор читателя
    public int Id { get; } = ++_counter;

    // Оставшееся количество данных для чтения
    public int DataCount { get; set; }

    // Событие, возникающее при чтении данных
    public event Action<int>? Read;

    // Событие, возникающее при завершении чтения всех данных
    public event Action? Deleted;

    public Reader(Buffer buffer)
    {
        // Инициализация оставшегося количества данных случайным числом
        DataCount = new Random().Next(1, 10);

        _buffer = buffer;

        // Создание нового потока для работы читателя
        _thread = new Thread(Work);
    }

    // Метод для запуска работы читателя
    public void Start() => _thread.Start();

    // Метод для остановки работы читателя
    public void Stop() => isStopped = true;

    // Метод, который выполняется в потоке читателя
    public void Work()
    {
        // Цикл работы читателя
        // Если isStopped == true, то поток завершит свою работу.
        // Завершает свою работу в методе Stop().
        while (isStopped != true)
        {
            // Задержка перед чтением следующего элемента
            Thread.Sleep(new Random().Next(1, 2));

            // Чтение данных из буфера
            ReadBuffer();
        }
    }

    // Метод для чтения данных из буфера
    private void ReadBuffer()
    {
        // Извлечение данных из буфера
        int result = _buffer.Pop();

        // Вызов события о прочтении данных
        Read?.Invoke(result);

        // Уменьшение количества оставшихся данных для чтения
        --DataCount;

        // Если все данные прочитаны, вызываем событие об удалении читателя
        // и останавливаем его работу
        if (DataCount == 0)
        {
            Deleted?.Invoke();
            Stop();
        }
    }
}
