namespace LabExam1.Models;

public class Payroll
{
    public int Id {get;set;}
    public int EmployeeId {get;set;}
    public DateOnly Date {get;set;}
    public int DaysWorked {get;set;}
    public decimal GrossPay {get;set;}
    public decimal Deduction {get;set;}
    public decimal NetPay {get;set;}

    public Employee? Employee {get;set;}
}
