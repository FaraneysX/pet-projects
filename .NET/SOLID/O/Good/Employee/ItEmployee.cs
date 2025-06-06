namespace Good.Employee;

/// <summary>
///     IT сотрудник.
/// </summary>
/// <param name="name">Имя.</param>
/// <param name="salary">Зарплата.</param>
internal class ItEmployee(string name, decimal salary) : IEmployee
{
    public string Name { get; set; } = name;
    public decimal Salary { get; set; } = salary;
}