using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMoney.Models;
using WebMoney.Services;

namespace WebMoney.Controllers
{
    public class ServiceController : Controller
    {
        private readonly EmailService emailService;
        private WebMoneyContext _context;
        public ServiceController(WebMoneyContext context, EmailService emailService)
        {
            _context = context;
            this.emailService = emailService;
        }
        public IActionResult Index()
        {
            var companies = _context.ServiceCompanies;
                return View(companies);
        }
    }
}
