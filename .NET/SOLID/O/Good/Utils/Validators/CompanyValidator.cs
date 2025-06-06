using Good.Constants;
using Good.Employee;

namespace Good.Utils.Validators;

/// <summary>
///     Валидация данных для IT компании.
/// </summary>
internal class CompanyValidator
{
    /// <summary>
    ///     Проверка допустимости зарплаты сотрудника.
    /// </summary>
    /// <param name="employee">Сотрудник.</param>
    /// <returns>
    ///     true - зарплата допустима;<br />
    ///     false - зарплата недопустима.
    /// </returns>
    public bool IsValidSalary(IEmployee employee)
    {
        return employee.Salary > 0 && employee.Salary <= ItCompanySalaries.MaxSalaries[employee.GetType()];
    }

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