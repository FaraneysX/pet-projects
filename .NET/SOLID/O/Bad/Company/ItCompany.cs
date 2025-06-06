using Bad.Employee;
using Bad.Logger;
using Bad.Utils.Validators;

namespace Bad.Company;

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
        switch (employee)
        {
            case HrEmployee when employee.Salary is < 70_000 or <= 0:
                logger.Log("HR сотрудник не может получать меньше 100 000.");

                return false;
            case ItEmployee when employee.Salary is < 100_000 or <= 0:
                logger.Log("IT сотрудник не может получать меньше 10 000.");

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