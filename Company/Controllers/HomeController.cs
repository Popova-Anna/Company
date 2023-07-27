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
        private readonly CompanyContext _context = new CompanyContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }



        public IActionResult Index()
        {
            List<ViewDepartments> viewDepartments = new List<ViewDepartments>();
            var companyContext = _context.Departments.Include(d => d.InverseParentDepartment).ToList();
            var sb = new StringBuilder();
            foreach (var department in companyContext)
            {
                if (department.ParentDepartmentId == null)
                {
                    sb.AppendLine( Handler.ParentStruct(department));
                }

            }
            ViewBag.Company = sb.ToString();
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