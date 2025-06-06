using Good.Employee;

namespace Good.Constants;

/// <summary>
///     Константы зарплат класса IT организации.
/// </summary>
internal static class ItCompanySalaries
{
    /// <summary>
    ///     Словарь максимальных зарплат сотрудников.
    /// </summary>
    public static readonly Dictionary<Type, decimal> MaxSalaries = new()
    {
        [typeof(ItEmployee)] = 100_000,
        [typeof(HrEmployee)] = 70_000
    };

    /// <summary>
    /// Установить максимальную зарплату для типа сотрудника.
    /// </summary>
    /// <param name="employeeType">Тип сотрудника.</param>
    /// <param name="salary">Новый максимум зарплаты.</param>
    /// <exception cref="ArgumentException">Если employeeType не является имплементацией IEmployee.</exception>
    public static void SetMaxSalary(Type employeeType, decimal salary)
    {
        if (!typeof(IEmployee).IsAssignableFrom(employeeType))
        {
            throw new ArgumentException($"Тип {employeeType} должен реализовывать IEmployee.");
        }

        MaxSalaries[employeeType] = salary;
    }
}