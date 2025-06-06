using Bad.Company;

namespace Bad.Utils;

/// <summary>
///     Расчет зарплат сотрудников.
/// </summary>
internal class SalaryCalculator
{
    /// <summary>
    ///     Вычислить общую сумму зарплат сотрудников.
    /// </summary>
    /// <returns>Общая сумма зарплат.</returns>
    public decimal GetTotalSalary(ICompany company)
    {
        return company.Employees.Sum(employee => employee.Salary);
    }
}