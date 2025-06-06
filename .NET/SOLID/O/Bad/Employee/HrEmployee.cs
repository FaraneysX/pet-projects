namespace Bad.Employee;

/// <summary>
///     HR сотрудник.
/// </summary>
/// <param name="name">Имя.</param>
/// <param name="salary">Зарплата.</param>
internal class HrEmployee(string name, decimal salary) : IEmployee
{
    public string Name { get; set; } = name;
    public decimal Salary { get; set; } = salary;
}