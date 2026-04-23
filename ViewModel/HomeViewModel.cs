using LabExam1.Models;
namespace LabExam1.ViewModel;

public class HomeViewModel
{
    public List<Employee> Employees {get;set;} = new();
    public List<Payroll> Payrolls {get;set;} = new();
    public int TotalEmployees {get;set;}
    public decimal TotalNetPay {get;set;}
    public decimal TotalGrossPay {get;set;}
    public decimal TotalDeductions {get;set;}
}
