namespace Bad.Logger;

/// <summary>
///     Логгер сообщений в консоль.
/// </summary>
internal class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}