using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMoney.Models
{
    public class ServiceCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CompanyType CompanyType { get; set; }
    }
    public enum CompanyType
    {
        БытовыеУслуги,
        ТелефонныйОператор,
        ИнтернетОператор
    }
}
