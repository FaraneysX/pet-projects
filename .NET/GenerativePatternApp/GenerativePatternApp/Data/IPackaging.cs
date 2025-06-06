namespace GenerativePatternApp.Data;

/// <summary>
/// Интерфейс, представляющий упаковку для товаров или грузов.
/// </summary>
public interface IPackaging
{
    /// <summary>
    /// Получает требования к упаковке, включая материалы, размеры и другие характеристики.
    /// </summary>
    string Requirements { get; }
    
    /// <summary>
    /// Получает инструкции по безопасности, связанные с упаковкой.
    /// </summary>
    string SafetyInstructions { get; }
}