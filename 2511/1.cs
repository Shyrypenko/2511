using System;
using System.Collections.Generic;
using System.Linq;

class Employee
{
    public string FullName { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
    public string Email { get; set; }
}

class EmployeeManager
{
    private List<Employee> employees = new();

    public void AddEmployee(Employee employee) => employees.Add(employee);

    public void RemoveEmployee(string email)
    {
        var employee = employees.FirstOrDefault(e => e.Email == email);
        if (employee != null) employees.Remove(employee);
        else Console.WriteLine("Employee not found.");
    }

    public void UpdateEmployee(string email, Employee updatedInfo)
    {
        var employee = employees.FirstOrDefault(e => e.Email == email);
        if (employee != null)
        {
            employee.FullName = updatedInfo.FullName;
            employee.Position = updatedInfo.Position;
            employee.Salary = updatedInfo.Salary;
            employee.Email = updatedInfo.Email;
        }
        else Console.WriteLine("Employee not found.");
    }

    public List<Employee> SearchEmployees(Func<Employee, bool> predicate) => employees.Where(predicate).ToList();

    public void SortEmployees(Func<Employee, object> keySelector) => employees = employees.OrderBy(keySelector).ToList();

    public void PrintEmployees()
    {
        foreach (var employee in employees)
        {
            Console.WriteLine($"{employee.FullName} | {employee.Position} | ${employee.Salary} | {employee.Email}");
        }
    }
}

class Program
{
    static void Main()
    {
        var manager = new EmployeeManager();
        manager.AddEmployee(new Employee { FullName = "Alice Smith", Position = "Manager", Salary = 70000, Email = "alice@example.com" });
        manager.AddEmployee(new Employee { FullName = "Bob Brown", Position = "Developer", Salary = 50000, Email = "bob@example.com" });

        Console.WriteLine("All employees:");
        manager.PrintEmployees();

        Console.WriteLine("\nSearch for employees with salary > 60000:");
        var highSalaryEmployees = manager.SearchEmployees(e => e.Salary > 60000);
        foreach (var e in highSalaryEmployees) Console.WriteLine($"{e.FullName} | {e.Salary}");

        Console.WriteLine("\nSorting by salary:");
        manager.SortEmployees(e => e.Salary);
        manager.PrintEmployees();
    }
}