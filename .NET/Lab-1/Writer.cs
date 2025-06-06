namespace Lab_1;

public class Writer
{
    private Thread thread; // Поток, в котором выполняется работа писателя
    private Buffer buffer; // Буфер, в который писатель записывает данные
    private bool isStopped = false; // Флаг для остановки работы писателя

    // Событие, возникающее перед записью данных в буфер
    public event Action<int>? Waiting;

    // Событие, возникающее после успешной записи данных в буфер
    public event Action<int>? Done;

    public Writer(Buffer buffer)
    {
        this.buffer = buffer;

        // Создание нового потока для работы писателя
        thread = new(Work);
    }

    // Метод для запуска работы писателя
    public void Run() => thread.Start();

    // Метод для остановки работы писателя
    public void Stop() => isStopped = true;

    // Метод, который выполняется в потоке писателя
    public void Work()
    {
        // Цикл работы писателя
        // Если isStopped == true, то поток завершит свою работу.
        // Завершает свою работу в методе Stop().
        while (isStopped != true)
        {
            // Запись данных в буфер
            Write();

            // Задержка перед следующей записью данных
            Thread.Sleep(2000);
        }
    }

    // Метод для записи данных в буфер
    private void Write()
    {
        // Генерация случайного числа для записи в буфер
        int value = new Random().Next(1, 100);

        // Вызов события о начале ожидания записи
        Waiting?.Invoke(value);

        // Запись данных в буфер
        // Если стек заполнен, поток блокируется внутри стека и будет ожидать,
        // пока в стеке не появится место.
        // Пока писатель заблокирован, он хранит в себе число, которое он хотел записать.
        buffer.Push(value);

        // Вызов события об успешной записи данных
        Done?.Invoke(value);
    }
}
