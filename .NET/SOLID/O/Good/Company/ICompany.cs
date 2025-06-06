using Good.Employee;

namespace Good.Company;

/// <summary>
///     Интерфейс компании.
/// </summary>
internal interface ICompany
{
    /// <summary>
    ///     Список сотрудников.
    /// </summary>
    List<IEmployee> Employees { get; set; }

    /// <summary>
    ///     Добавить сотрудника в список.
    /// </summary>
    /// <param name="employee">Интерфейс сотрудника.</param>
    /// <returns>
    ///     true - удалось добавить сотрудника в список;<br />
    ///     false - не удалось добавить сотрудника в список.
    /// </returns>
    bool AddEmployee(IEmployee employee);

    /// <summary>
    ///     Удалить сотрудника из списка.
    /// </summary>
    /// <param name="index">Индекс сотрудника в списке.</param>
    /// <returns>
    ///     true - удалось удалить сотрудника из списка;<br />
    ///     false - не удалось удалить сотрудника из списка.
    /// </returns>
    bool RemoveEmployee(int index);
}