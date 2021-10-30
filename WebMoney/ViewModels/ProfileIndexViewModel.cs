using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMoney.Models;

namespace WebMoney.ViewModels
{
    public class ProfileIndexViewModel
    {
        public User User { get; set; }
        public BankAccount Wallet { get; set; }
    }
}
