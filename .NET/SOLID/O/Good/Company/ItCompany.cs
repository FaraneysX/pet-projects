using Good.Constants;
using Good.Employee;
using Good.Logger;
using Good.Utils.Validators;

namespace Good.Company;

/// <summary>
///     IT компания.
/// </summary>
/// <param name="employees">Список сотрудников.</param>
/// <param name="logger">Логгер.</param>
internal class ItCompany(List<IEmployee> employees, CompanyValidator validator, ILogger logger) : ICompany
{
    public List<IEmployee> Employees { get; set; } = employees;

    public bool AddEmployee(IEmployee employee)
    {
        if (!validator.IsValidSalary(employee))
        {
            logger.Log($"Зарплата у {employee.GetType().Name} должна быть от 0 до {ItCompanySalaries.MaxSalaries[employee.GetType()]:N0}. (указана: {employee.Salary:N0})");

            return false;
        }

        Employees.Add(employee);

        logger.Log($"Сотрудник {employee.Name} {employee.GetType().Name} с зарплатой {employee.Salary:N0} успешно добавлен в список.");

        return true;
    }

    public bool RemoveEmployee(int index)
    {
        if (!validator.IsValidIndex(index, Employees.Count))
        {
            logger.Log($"Сотрудник с индексом {index} не существует.");

            return false;
        }

        Employees.RemoveAt(index);

        logger.Log($"Сотрудник с ID {index} успешно удален из списка.");

        return true;
    }
}