namespace Bad.Utils.Validators;

/// <summary>
///     Валидация данных для IT компании.
/// </summary>
internal class CompanyValidator
{
    /// <summary>
    ///     Проверка допустимости индекса сотрудника для списка сотрудников.
    /// </summary>
    /// <param name="index">Проверяемый индекс сотрудника.</param>
    /// <param name="size">Размер списка сотрудников.</param>
    /// <returns>
    ///     true - индекс допустим;<br />
    ///     false - индекс недопустим.
    /// </returns>
    public bool IsValidIndex(int index, int size)
    {
        return index >= 0 && size > index;
    }
}