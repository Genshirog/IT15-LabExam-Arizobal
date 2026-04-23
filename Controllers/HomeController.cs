using LabExam1.Data;
using LabExam1.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var employees = await _context.employees.ToListAsync();
        var payrolls = await _context.payrolls.Include(p => p.Employee).ToListAsync();

        var vm = new HomeViewModel
        {
            Employees = employees,
            Payrolls = payrolls,
            TotalEmployees = employees.Count,
            TotalGrossPay = payrolls.Sum(p => p.GrossPay),
            TotalDeductions = payrolls.Sum(p => p.Deduction),
            TotalNetPay = payrolls.Sum(p => p.NetPay)
        };

        return View(vm);
    }
}