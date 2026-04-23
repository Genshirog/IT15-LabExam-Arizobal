using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabExam1.Data;
using LabExam1.Models;

namespace LabExam1.Controllers
{
    public class PayrollController : Controller
    {
        private readonly AppDbContext _context;

        public PayrollController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Payroll
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.payrolls.Include(p => p.Employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Payroll/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payroll = await _context.payrolls
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payroll == null)
            {
                return NotFound();
            }

            return View(payroll);
        }

        // GET: Payroll/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.employees, "Id", "Id");
            return View();
        }

        // POST: Payroll/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,Date,DaysWorked,Deduction")] Payroll payroll)
        {
            if (ModelState.IsValid)
            {   
                var employee = await _context.employees.FindAsync(payroll.EmployeeId);
                if (employee == null)
                {
                    ModelState.AddModelError("EmployeeId", "Employee not found.");
                }
                else
                {
                    payroll.GrossPay = payroll.DaysWorked * employee.DailyRate;
                    payroll.NetPay = payroll.GrossPay - payroll.Deduction;
                    _context.Add(payroll);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["EmployeeId"] = new SelectList(_context.employees, "Id", "FirstName", payroll.EmployeeId);
            return View(payroll);
        }

        // GET: Payroll/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payroll = await _context.payrolls.FindAsync(id);
            if (payroll == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.employees, "Id", "FirstName", payroll.EmployeeId);
            return View(payroll);
        }

        // POST: Payroll/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,Date,DaysWorked,Deduction")] Payroll payroll)
        {
            if (id != payroll.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                     var employee = await _context.employees.FindAsync(payroll.EmployeeId);
                    if (employee == null)
                    {
                        ModelState.AddModelError("EmployeeId", "Employee not found.");
                    }
                    else
                    {
                        payroll.GrossPay = payroll.DaysWorked * employee.DailyRate;
                        payroll.NetPay = payroll.GrossPay - payroll.Deduction;
                        _context.Update(payroll);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayrollExists(payroll.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.employees, "Id", "FirstName", payroll.EmployeeId);
            return View(payroll);
        }

        // GET: Payroll/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payroll = await _context.payrolls
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payroll == null)
            {
                return NotFound();
            }

            return View(payroll);
        }

        // POST: Payroll/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payroll = await _context.payrolls.FindAsync(id);
            if (payroll != null)
            {
                _context.payrolls.Remove(payroll);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayrollExists(int id)
        {
            return _context.payrolls.Any(e => e.Id == id);
        }
    }
}
