using System;
using WebMoney.Enums;

namespace WebMoney.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public string WalletFrom { get; set; }
        public string WalletTo { get; set; }
        public double MoneyCount { get; set; }
        public DateTime DateTime { get; set; }
        public TransactionType TransactionType { get; set; }

    }
   
}
