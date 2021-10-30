using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMoney.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string UniqueNumber { get; set; }
        public string UserId { get; set; }
        public double MoneyCount { get; set; }
    }
}
