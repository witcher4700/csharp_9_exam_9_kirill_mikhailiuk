using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMoney.Models
{
    public class CompanyUser
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string PhoneNumber { get; set; }
        public string Walletnumber { get; set; }
    }
}
