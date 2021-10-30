using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace WebMoney.ViewModels
{
    public class HistoryViewModel
    {
        public List<WebMoney.Models.Transaction> Transactions { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }
}
