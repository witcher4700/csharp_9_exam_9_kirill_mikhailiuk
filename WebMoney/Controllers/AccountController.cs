using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMoney.Models;
using WebMoney.Services;
using WebMoney.ViewModels;

namespace WebMoney.Controllers
{
    public class AccountController : Controller
    {
        private WebMoneyContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly EmailService emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController( UserManager<User> userManager, WebMoneyContext context, SignInManager<User> signInManager, EmailService emailService, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            this.emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                User user = new User
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    UniqueСode = GetUniqueСode()
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                    await _signInManager.SignInAsync(user, false);
                    var bank = new BankAccount()
                    {
                        UniqueNumber = GetUniqueNumber(),
                        UserId = user.Id,
                        MoneyCount = 100
                    };
                    emailService.SendEmailCustom(user.Email, "Ваш уникальный код для входа в аккаунт: "+user.UniqueСode + " Ваш номер счёта: "+bank.UniqueNumber);
                    _context.BankAccounts.Add(bank);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Profile");
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _context.Users.FirstOrDefault(u=>u.UniqueСode == model.UniqueСode) ;
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(
                    user,
                    model.Password,
                    model.RememberMe,
                    false
                    );
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Profile");
                }
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Bank");
        }
        public string GetUniqueСode()
        {
            Random rand = new Random();
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            string word = "";
            int num_letters = 6;
            for (int j = 1; j <= num_letters; j++)
            {               
                int letter_num = rand.Next(0, letters.Length - 1);
                word += letters[letter_num];
            }
            var users = _context.Users.ToList();
            foreach (var user in users)
            {
                if(user.UniqueСode == word)
                {
                    return GetUniqueСode();
                }
            }
            return word;
        }
        public string GetUniqueNumber()
        {
            Random rand = new Random();
            char[] letters = "1234567890".ToCharArray();
            string word = "";
            int num_letters = 12;
            for (int j = 1; j <= num_letters; j++)
            {
                int letter_num = rand.Next(0, letters.Length - 1);
                word += letters[letter_num];
            }
            var users = _context.Users.ToList();
            foreach (var user in users)
            {
                if (user.UniqueСode == word)
                {
                    return GetUniqueNumber();
                }
            }
            return word;
        }
    }
}
