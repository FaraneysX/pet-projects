using Good.Company;
using Good.Constants;
using Good.Employee;
using Good.Logger;
using Good.Utils;
using Good.Utils.Validators;

var companyValidator = new CompanyValidator();
var logger = new ConsoleLogger();
var company = new ItCompany([], companyValidator, logger);

company.AddEmployee(new HrEmployee("Петр", 10_000));
company.AddEmployee(new HrEmployee("Иван", 25_000));

company.AddEmployee(new ItEmployee("Дмитрий", 250_000));
company.AddEmployee(new ItEmployee("Олег", 100_000));

company.AddEmployee(new ItEmployee("Миллионер", 1_000_000));

var salaryCalculator = new SalaryCalculator();

logger.Log($"Общая сумма зарплат компании: {salaryCalculator.GetTotalSalary(company):N0}.");

ItCompanySalaries.SetMaxSalary(typeof(ItEmployee), 1_000_000);

company.AddEmployee(new ItEmployee("Миллионер", 1_000_000));
