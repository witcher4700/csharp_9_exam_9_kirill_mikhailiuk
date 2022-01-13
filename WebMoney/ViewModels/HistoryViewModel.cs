using System.Collections.Generic;

namespace WebMoney.ViewModels
{
    public class HistoryViewModel
    {
        public List<Entities.Transaction> Transactions { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }
}
