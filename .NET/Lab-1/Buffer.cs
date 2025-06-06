namespace Lab_1;

public class Buffer
{
    private readonly Stack<int> data; // Стек данных
    private readonly object lockObject = new(); // Объект для блокировки
    private readonly SemaphoreSlim semaphore; // Семафор для управления доступом к данным
    private readonly int maxSize; // Максимальный размер буфера

    public Buffer(int size)
    {
        data = new(size);
        semaphore = new(0, size); // Создание семафора с начальным значением 0 и максимальным значением size
        maxSize = size;
    }

    public void Push(int value)
    {
        lock (lockObject) // Блокировка доступа к данным
        {
            data.Push(value); // Добавление элемента в стек
        }

        // Проверка, достигнуто ли максимальное значение семафора
        // Если значение семафона (количество элементов в стеке) максимально,
        // то блокируем поток.
        if (semaphore.CurrentCount == maxSize)
        {
            lock (lockObject) // Блокировка доступа к условной переменной
            {
                // Ожидаем освобождения семафора.
                Monitor.Wait(lockObject);
            }
        }

        // Увеличение значения семафора
        semaphore.Release();
    }

    public int Pop()
    {
        // Ожидание доступа к буферу (делаем минус 1 у семафора, чтобы в случае,
        // если поток в методе Push() был заблокирован - значение семафора достигло максимума,
        // то есть количество элементов в стеке достигло максимума, то он будет разблокирован.
        semaphore.Wait();

        lock (lockObject) // Блокировка доступа к данным
        {
            int value = data.Pop(); // Извлечение элемента из стека

            // Если счетчик семафора был на максимуме, освобождаем поток, который работает в методе Push().
            if (semaphore.CurrentCount >= 0)
            {
                Monitor.Pulse(lockObject); // Сигнализация другому потоку
            }

            return value;
        }
    }

    // Метод для преобразования буфера в массив
    public int[] ToArray()
    {
        return [.. data]; // Преобразование стека в массив и возвращение его
    }
}
