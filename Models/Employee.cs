using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace LabExam1.Models;

public class Employee
{
    [Required]
    public int Id {get;set;}
    public string FirstName {get;set;} = string.Empty;
    public string LastName {get;set;} = string.Empty;
    public Positions Position {get;set;} = Positions.Employee;
    public string Department {get;set;} = string.Empty;
    public decimal DailyRate {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.UtcNow;
    public DateTime UpdatedAt {get;set;} = DateTime.UtcNow;

    public ICollection<Payroll>? Payrolls {get;set;} = [];
}

public enum Positions
{
    Employee,
    Manager,
    Admin
}
