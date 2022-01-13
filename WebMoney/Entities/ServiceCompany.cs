using WebMoney.Enums;

namespace WebMoney.Entities
{
    public class ServiceCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CompanyType CompanyType { get; set; }
    }
}
