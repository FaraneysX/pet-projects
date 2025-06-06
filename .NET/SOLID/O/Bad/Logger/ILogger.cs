namespace Bad.Logger;

/// <summary>
///     Интерфейс логгера.
/// </summary>
internal interface ILogger
{
    /// <summary>
    ///     Запись сообщения в консоль.
    /// </summary>
    /// <param name="message">Сообщение для логирования.</param>
    void Log(string message);
}