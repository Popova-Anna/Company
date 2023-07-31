using Company.Models;
using Company.Repository;
using Company.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text;
using System.Web;

namespace Company.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CompanyContext _context = new();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }



        public IActionResult Index()
        {
            List<ViewDepartments> viewDepartments = new();
            var companyContext = _context.Departments.Include(d => d.InverseParentDepartment).Include(e=>e.Empoyees).ToList();
            var sb = new StringBuilder();
            foreach (var department in companyContext)
            {
                if (department.ParentDepartmentId == null)
                {
                    sb.AppendLine( Handler.ParentStruct(department));
                }

            }
            ViewBag.Company = sb.ToString();
            ViewData["Title"] = "Структура компании";
            var countEmployee =  _context.Empoyees.ToList().Count;
            ViewBag.Employee = countEmployee;
            ViewBag.AverageAge = _context.Database.SqlQuery<int>($"SELECT AVG(DATEDIFF(YEAR, DateOfBirth, GETDATE())) AS average_age FROM dbo.Empoyee").ToList();
            return View(companyContext);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}