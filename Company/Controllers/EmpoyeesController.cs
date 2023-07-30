using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Company.Models;

namespace Company.Controllers
{
    public class EmpoyeesController : Controller
    {
        private readonly CompanyContext _context;

        public EmpoyeesController(CompanyContext context)
        {
            _context = context;
        }

        // GET: Empoyees
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Сотрудники компании";
            var companyContext = _context.Empoyees.Include(e => e.Department).ToListAsync();
            return View(await companyContext);
        }

        // GET: Empoyees/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            
            if (id == null || _context.Empoyees == null)
            {
                return NotFound();
            }

            var empoyee = await _context.Empoyees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empoyee == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Подробная информация о сотруднике "+empoyee.SurName+" " +empoyee.FirstName + " " + empoyee.Patronymic;
            return View(empoyee);
        }

        // GET: Empoyees/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["Title"] = "Добавить сотрудника ";
            return View();
        }

        // POST: Empoyees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,SurName,Patronymic,DateOfBirth,DocSeries,DocNumber,Position,DepartmentId")] Empoyee empoyee)
        {
            if (empoyee!= null)
            {
                _context.Add(empoyee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", empoyee.DepartmentId);
            ViewData["Title"] = "Добавить сотрудника ";
            return View(empoyee);
        }

        // GET: Empoyees/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Empoyees == null)
            {
                return NotFound();
            }

            var empoyee = await _context.Empoyees.FindAsync(id);
            if (empoyee == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", empoyee.DepartmentId);
            ViewData["Title"] = "Редактирование информации о сотруднике " + empoyee.SurName + " " + empoyee.FirstName + " " + empoyee.Patronymic;
            return View(empoyee);
        }

        // POST: Empoyees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,FirstName,SurName,Patronymic,DateOfBirth,DocSeries,DocNumber,Position,DepartmentId")] Empoyee empoyee)
        {
            if (id != empoyee.Id)
            {
                return NotFound();
            }

            if (empoyee != null)
            {
                try
                {
                    _context.Update(empoyee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpoyeeExists(empoyee.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", empoyee.DepartmentId);
            ViewData["Title"] = "Редактирование информации о сотруднике " + empoyee.SurName + " " + empoyee.FirstName + " " + empoyee.Patronymic;
            return View(empoyee);
        }

        // GET: Empoyees/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Empoyees == null)
            {
                return NotFound();
            }

            var empoyee = await _context.Empoyees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empoyee == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Удалить сотрудника " + empoyee.SurName + " " + empoyee.FirstName + " " + empoyee.Patronymic;
            return View(empoyee);
        }

        // POST: Empoyees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Empoyees == null)
            {
                return Problem("База сотрудников пустая!");
            }
            var empoyee = await _context.Empoyees.FindAsync(id);
            if (empoyee != null)
            {
                _context.Empoyees.Remove(empoyee);
            }
            ViewData["Title"] = "Удалить сотрудника " + empoyee.SurName + " " + empoyee.FirstName + " " + empoyee.Patronymic;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpoyeeExists(decimal id)
        {
          return (_context.Empoyees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
