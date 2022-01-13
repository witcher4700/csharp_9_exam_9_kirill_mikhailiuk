using Microsoft.AspNetCore.Mvc;
using WebMoney.Models;
using WebMoney.Services;

namespace WebMoney.Controllers
{
    public class ServiceController : Controller
    {
        private ApplicationDbContext _context;
        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var companies = _context.ServiceCompanies;
            return View(companies);
        }
    }
}