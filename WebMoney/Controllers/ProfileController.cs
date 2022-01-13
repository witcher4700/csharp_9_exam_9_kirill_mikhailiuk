using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebMoney.Models;
using WebMoney.ViewModels;

namespace WebMoney.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext _context;
        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = new ProfileIndexViewModel()
            {
                User = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name),
                Wallet = _context.BankAccounts.FirstOrDefault(b=>b.UserId == _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Id)
            };
            return View(model);
        }
    }
}
