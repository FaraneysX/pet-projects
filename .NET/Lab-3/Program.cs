// #13
// Вывести информацию о процессах, имеющих
// наибольший суммарный размер модулей.

using Lab_3;

ToolHelp32 tool = new();

foreach (var process in tool.GetProcessesWithMaxModuleSize())
{
    Console.WriteLine($"Название процесса: {process.szExeFile}, ID процесса: {process.th32ProcessID}\n" +
        $"Суммарный размер модулей: {ToolHelp32.TotalModuleSize(process.th32ProcessID)}");
}