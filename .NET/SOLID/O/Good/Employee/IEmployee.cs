using Good.Constants;

namespace Good.Employee;

/// <summary>
///     Интерфейс сотрудника.
/// </summary>
internal interface IEmployee
{
    /// <summary>
    ///     Имя.
    /// </summary>
    string Name { get; }

    /// <summary>
    ///     Зарплата.
    /// </summary>
    decimal Salary { get; }
}