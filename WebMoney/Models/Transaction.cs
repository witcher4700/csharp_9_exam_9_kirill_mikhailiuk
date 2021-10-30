using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMoney.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string WalletFrom { get; set; }
        public string WalletTo { get; set; }
        public double MoneyCount { get; set; }
        public DateTime DateTime { get; set; }
        public TransActionType TransActionType { get; set; }

    }
    public enum TransActionType
    {
        Перевод,
        Снятие,
        Пополнение,
        ОплатаУслуг
    }
}
