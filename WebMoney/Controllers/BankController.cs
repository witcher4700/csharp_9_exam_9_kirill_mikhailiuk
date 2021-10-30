using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMoney.Models;
using WebMoney.Services;
using WebMoney.ViewModels;

namespace WebMoney.Controllers
{
    public class BankController : Controller
    {
        private readonly EmailService emailService;
        private WebMoneyContext _context;
        public BankController(WebMoneyContext context, EmailService emailService)
        {
            _context = context;
            this.emailService = emailService;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {

                return RedirectToAction("Index", "Profile");
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        public JsonResult AnonymousTransfer(AnonymousTransferViewModel model)
        {
            var transfer = new Transaction()
            {
                MoneyCount = model.MoneyCount,
                WalletTo = model.WalletNumber,
                WalletFrom = "Anonymous",
                DateTime = DateTime.Now,
                TransActionType = TransActionType.Перевод
            };
            var wallet = _context.BankAccounts.FirstOrDefault(b=>b.UniqueNumber == model.WalletNumber);
            if(wallet == null)
            {
                return Json("Такого кошелька не существует!");
            }
            else
            {
                _context.Entry(wallet).State = EntityState.Modified;
                wallet.MoneyCount += transfer.MoneyCount;
                _context.Transactions.Add(transfer);
                _context.SaveChanges();
                var user = _context.Users.FirstOrDefault(u => u.Id == wallet.UserId);
                emailService.SendEmailCustom(user.Email, "На ваш счёт был совершен перевод от анонимного пользователя. На ваш счёт поступило: "+ transfer.MoneyCount+ "$");
                return Json("200");
            }
            
           
        }
        [HttpPost]
        public JsonResult DefaultTransfer(AnonymousTransferViewModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var myWallet = _context.BankAccounts.FirstOrDefault(b => b.UserId == user.Id);
            var transfer = new Transaction()
            {
                MoneyCount = model.MoneyCount,
                WalletTo = model.WalletNumber,
                WalletFrom = myWallet.UniqueNumber,
                DateTime = DateTime.Now,
                TransActionType = TransActionType.Перевод
            };
            var wallet = _context.BankAccounts.FirstOrDefault(b => b.UniqueNumber == model.WalletNumber);
            if(myWallet.MoneyCount < model.MoneyCount)
            {
                return Json("На вашем кошельке недостаточно денег!");
            }
            if (wallet == null)
            {
                return Json("Такого кошелька не существует!");
            }
            else
            {
                _context.Entry(wallet).State = EntityState.Modified;
                _context.Entry(myWallet).State = EntityState.Modified;
                wallet.MoneyCount += transfer.MoneyCount;
                myWallet.MoneyCount -= transfer.MoneyCount;
                _context.Transactions.Add(transfer);
                _context.SaveChanges();
                var anotherUser = _context.Users.FirstOrDefault(u => u.Id == wallet.UserId);
                emailService.SendEmailCustom(user.Email, "Вы совершили перевод на счёт пользователя" + anotherUser.UserName + " Вы перевели: " + transfer.MoneyCount + "$");
                emailService.SendEmailCustom(user.Email, "На ваш счёт был совершен перевод от пользователя "+user.UserName + " На ваш счёт поступило: " + transfer.MoneyCount + "$");
                return Json("200");
            }
        }

        [HttpPost] 
        public JsonResult GetMoney(double moneyCount)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var myWallet = _context.BankAccounts.FirstOrDefault(b => b.UserId == user.Id);
            var transfer = new Transaction()
            {
                MoneyCount = moneyCount,
                WalletTo = myWallet.UniqueNumber,
                WalletFrom = myWallet.UniqueNumber,
                DateTime = DateTime.Now,
                TransActionType = TransActionType.Снятие
            };
            if (myWallet.MoneyCount < moneyCount)
            {
                return Json("На вашем кошельке недостаточно денег!");
            }
            _context.Entry(myWallet).State = EntityState.Modified;
            myWallet.MoneyCount -= moneyCount;
            _context.Transactions.Add(transfer);
            _context.SaveChanges();
            emailService.SendEmailCustom(user.Email, "Вы сняли с вашего счёта" + transfer.MoneyCount + "$"+ "На вашем счету сейчас "+myWallet.MoneyCount+"$");
            return Json("200");
        }
        [HttpPost] 
        public JsonResult AddMoney(double moneyCount)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var myWallet = _context.BankAccounts.FirstOrDefault(b => b.UserId == user.Id);
            var transfer = new Transaction()
            {
                MoneyCount = moneyCount,
                WalletTo = myWallet.UniqueNumber,
                WalletFrom = myWallet.UniqueNumber,
                DateTime = DateTime.Now,
                TransActionType = TransActionType.Пополнение
            };
            _context.Entry(myWallet).State = EntityState.Modified;
            myWallet.MoneyCount += moneyCount;
            _context.Transactions.Add(transfer);
            _context.SaveChanges();
            emailService.SendEmailCustom(user.Email, "Вы пополнили свой счёт" + transfer.MoneyCount + "$" + "На вашем счету сейчас " + myWallet.MoneyCount + "$");
            return Json("200");
        }
        public IActionResult History(string dateFrom, string dateTo)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var myWallet = _context.BankAccounts.FirstOrDefault(b => b.UserId == user.Id);
            var history = _context.Transactions.Where(h => h.WalletFrom == myWallet.UniqueNumber || h.WalletTo == myWallet.UniqueNumber).ToList();
            if (!String.IsNullOrEmpty(dateFrom))
            {
                history = history.Where(p => p.DateTime > Convert.ToDateTime(dateFrom)).ToList();
            }
            if (!String.IsNullOrEmpty(dateTo))
            {
                history = history.Where(p => p.DateTime < Convert.ToDateTime(dateTo)).ToList();
            }
            
            var historyViewModel = new HistoryViewModel()
            {
                Transactions = history
            };
            return View(historyViewModel);
        }
        [HttpPost]
        public JsonResult Pay(int id, double moneyCount)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var myWallet = _context.BankAccounts.FirstOrDefault(b => b.UserId == user.Id);
            var companyUser = _context.CompanyUsers.FirstOrDefault(c => c.CompanyId == id);
            var transfer = new Transaction()
            {
                MoneyCount = moneyCount,
                WalletTo = companyUser.Walletnumber,
                WalletFrom = myWallet.UniqueNumber,
                DateTime = DateTime.Now,
                TransActionType = TransActionType.ОплатаУслуг
            };
            var wallet = _context.BankAccounts.FirstOrDefault(b => b.UniqueNumber == companyUser.Walletnumber);
            if (myWallet.MoneyCount < moneyCount)
            {
                return Json("На вашем кошельке недостаточно денег!");
            }

            else
            {
                _context.Entry(wallet).State = EntityState.Modified;
                _context.Entry(myWallet).State = EntityState.Modified;
                wallet.MoneyCount += transfer.MoneyCount;
                myWallet.MoneyCount -= transfer.MoneyCount;
                _context.Transactions.Add(transfer);
                _context.SaveChanges();
                var anotherUser = _context.Users.FirstOrDefault(u => u.Id == wallet.UserId);
                emailService.SendEmailCustom(user.Email, "Вы совершили оплату услуг компании: " + _context.ServiceCompanies.FirstOrDefault(c=>c.Id == id).Name + " Вы потратили: " + transfer.MoneyCount + "$");
                return Json("200");
            }
        }
    }
}
