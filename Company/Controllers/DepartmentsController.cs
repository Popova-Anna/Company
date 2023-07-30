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
    public class DepartmentsController : Controller
    {
        private readonly CompanyContext _context;

        public DepartmentsController(CompanyContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            var companyContext = _context.Departments.Include(d => d.ParentDepartment);
            ViewData["Title"] = "Отделы компании";
            return View(await companyContext.ToListAsync());
        }

        public IActionResult Employee(Guid? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }
            
            var department = _context.Departments
                .Include(d => d.ParentDepartment)
                .Include(d=>d.Empoyees)
                .FirstOrDefault(m => m.Id == id);

            if (department == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Сотрудники отдела " + department.Name;
            return View(department);
        }


        // GET: Departments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .Include(d => d.ParentDepartment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Подробная информация об отделе " + department.Name;
            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            ViewData["ParentDepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["Title"] = "Добавление отдела";
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Code,ParentDepartmentId")] Department department)
        {
            if (ModelState.IsValid)
            {
                department.Id = Guid.NewGuid();
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentDepartmentId"] = new SelectList(_context.Departments, "Id", "Name", department.ParentDepartmentId);
            ViewData["Title"] = "Добавление отдел";
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            ViewData["ParentDepartmentId"] = new SelectList(_context.Departments, "Id", "Name", department.ParentDepartmentId);
            ViewData["Title"] = "Редактирование информации об отделе "+department.Name;
            return View(department);
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Code,ParentDepartmentId")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
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
            ViewData["ParentDepartmentId"] = new SelectList(_context.Departments, "Id", "Name", department.ParentDepartmentId);
            ViewData["Title"] = "Редактирование информации об отделе " + department.Name;
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .Include(d => d.ParentDepartment)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (department == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Удаление отдела " + department.Name;
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            //Проверка на наличие отделов
            if (_context.Departments == null)
            {
                TempData["Problem"] = "База отделов пустая!";
                return RedirectToAction("Delete", id);
            }
            //Проверка на наличие сотрудников в отделе
            if (_context.Empoyees.Where(d => d.DepartmentId == id).ToList().Count > 0)
            {
                TempData["Problem"] = "В отделе есть сотрудники! Очистите отдел, перед удалением!";
                return RedirectToAction("Delete", id);
            }           
           //Проверка на наличие зависимых отделов
            if (_context.Departments.Include(d => d.ParentDepartment).Where(d=>d.ParentDepartmentId==id).ToList().Count > 0)
            {
                TempData["Problem"] = "У отдела есть зависимые отделы! Очистите отделы!";
                return RedirectToAction("Delete", id);
            }
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(Guid id)
        {
          return (_context.Departments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
