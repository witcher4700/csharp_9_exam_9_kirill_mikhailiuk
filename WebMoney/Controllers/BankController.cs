using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebMoney.Entities;
using WebMoney.Enums;
using WebMoney.Models;
using WebMoney.Services;
using WebMoney.ViewModels;

namespace WebMoney.Controllers
{
    public class BankController : Controller
    {
        private readonly EmailService emailService;
        private ApplicationDbContext _context;
        public BankController(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            this.emailService = serviceProvider.GetRequiredService<EmailService>();
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Profile");
            else
                return View();
        }
        [HttpPost]
        public async Task<JsonResult> AnonymousTransfer(AnonymousTransferViewModel model)
        {
            var transfer = new Transaction()
            {
                MoneyCount = model.MoneyCount,
                WalletTo = model.WalletNumber,
                WalletFrom = "Anonymous",
                DateTime = DateTime.Now,
                TransactionType = TransactionType.Transfer
            };
            var wallet = await _context.BankAccounts.FirstOrDefaultAsync(b => b.UniqueNumber == model.WalletNumber);
            if (wallet == null)
                return Json("Такого кошелька не существует!");
            else
            {
                wallet.MoneyCount += transfer.MoneyCount;
                await _context.Transactions.AddAsync(transfer);
                await _context.SaveChangesAsync();
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == wallet.UserId);
                emailService.SendEmailCustom(user.Email, "На ваш счёт был совершен перевод от анонимного пользователя. На ваш счёт поступило: " + transfer.MoneyCount + "$");
                return Json("200");
            }


        }
        [Authorize]
        [HttpPost]
        public async Task<JsonResult> DefaultTransfer(AnonymousTransferViewModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var myWallet = await _context.BankAccounts.FirstOrDefaultAsync(b => b.UserId == user.Id);
            var transfer = new Transaction()
            {
                MoneyCount = model.MoneyCount,
                WalletTo = model.WalletNumber,
                WalletFrom = myWallet.UniqueNumber,
                DateTime = DateTime.Now,
                TransactionType = TransactionType.Transfer
            };
            var wallet = await _context.BankAccounts.FirstOrDefaultAsync(b => b.UniqueNumber == model.WalletNumber);
            if (myWallet.MoneyCount < model.MoneyCount)
                return Json("На вашем кошельке недостаточно денег!");
            if (wallet == null)
                return Json("Такого кошелька не существует!");
            else
            {
                wallet.MoneyCount += transfer.MoneyCount;
                myWallet.MoneyCount -= transfer.MoneyCount;
                await _context.Transactions.AddAsync(transfer);
                await _context.SaveChangesAsync();
                var anotherUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == wallet.UserId);
                emailService.SendEmailCustom(user.Email, "Вы совершили перевод на счёт пользователя" + anotherUser.UserName + " Вы перевели: " + transfer.MoneyCount + "$");
                emailService.SendEmailCustom(user.Email, "На ваш счёт был совершен перевод от пользователя " + user.UserName + " На ваш счёт поступило: " + transfer.MoneyCount + "$");
                return Json("200");
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<JsonResult> GetMoney(double moneyCount)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var myWallet = await _context.BankAccounts.FirstOrDefaultAsync(b => b.UserId == user.Id);
            var transfer = new Transaction()
            {
                MoneyCount = moneyCount,
                WalletTo = myWallet.UniqueNumber,
                WalletFrom = myWallet.UniqueNumber,
                DateTime = DateTime.Now,
                TransactionType = TransactionType.Withdrawal
            };
            if (myWallet.MoneyCount < moneyCount)
                return Json("На вашем кошельке недостаточно денег!");
            myWallet.MoneyCount -= moneyCount;
            await _context.Transactions.AddAsync(transfer);
            await _context.SaveChangesAsync();
            emailService.SendEmailCustom(user.Email, "Вы сняли с вашего счёта" + transfer.MoneyCount + "$" + "На вашем счету сейчас " + myWallet.MoneyCount + "$");
            return Json("200");
        }
        [Authorize]
        [HttpPost]
        public async Task<JsonResult> AddMoney(double moneyCount)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var myWallet = await _context.BankAccounts.FirstOrDefaultAsync(b => b.UserId == user.Id);
            var transfer = new Transaction()
            {
                MoneyCount = moneyCount,
                WalletTo = myWallet.UniqueNumber,
                WalletFrom = myWallet.UniqueNumber,
                DateTime = DateTime.Now,
                TransactionType = TransactionType.Refill
            };
            myWallet.MoneyCount += moneyCount;
            await _context.Transactions.AddAsync(transfer);
            await _context.SaveChangesAsync();
            emailService.SendEmailCustom(user.Email, "Вы пополнили свой счёт" + transfer.MoneyCount + "$" + "На вашем счету сейчас " + myWallet.MoneyCount + "$");
            return Json("200");
        }
        [Authorize]
        public async Task<IActionResult> History(string dateFrom, string dateTo)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var myWallet = await _context.BankAccounts.FirstOrDefaultAsync(b => b.UserId == user.Id);
            var history = await _context.Transactions.Where(h => h.WalletFrom == myWallet.UniqueNumber || h.WalletTo == myWallet.UniqueNumber).ToListAsync();
            if (!String.IsNullOrEmpty(dateFrom))
                history = history.Where(p => p.DateTime > Convert.ToDateTime(dateFrom)).ToList();
            if (!String.IsNullOrEmpty(dateTo))
                history = history.Where(p => p.DateTime < Convert.ToDateTime(dateTo)).ToList();

            var historyViewModel = new HistoryViewModel()
            {
                Transactions = history
            };
            return View(historyViewModel);
        }
        [Authorize]
        [HttpPost]
        public async Task<JsonResult> Pay(int id, double moneyCount)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var myWallet = await _context.BankAccounts.FirstOrDefaultAsync(b => b.UserId == user.Id);
            var companyUser = await _context.CompanyUsers.FirstOrDefaultAsync(c => c.CompanyId == id);
            var transfer = new Transaction()
            {
                MoneyCount = moneyCount,
                WalletTo = companyUser.Walletnumber,
                WalletFrom = myWallet.UniqueNumber,
                DateTime = DateTime.Now,
                TransactionType = TransactionType.PaymentForServices
            };
            var wallet = await _context.BankAccounts.FirstOrDefaultAsync(b => b.UniqueNumber == companyUser.Walletnumber);
            if (myWallet.MoneyCount < moneyCount)
                return Json("На вашем кошельке недостаточно денег!");
            else
            {
                wallet.MoneyCount += transfer.MoneyCount;
                myWallet.MoneyCount -= transfer.MoneyCount;
                await _context.Transactions.AddAsync(transfer);
                await _context.SaveChangesAsync();
                var anotherUser = _context.Users.FirstOrDefault(u => u.Id == wallet.UserId);
                emailService.SendEmailCustom(user.Email, "Вы совершили оплату услуг компании: " + (await _context.ServiceCompanies.FirstOrDefaultAsync(c => c.Id == id)).Name + " Вы потратили: " + transfer.MoneyCount + "$");
                return Json("200");
            }
        }
    }
}
