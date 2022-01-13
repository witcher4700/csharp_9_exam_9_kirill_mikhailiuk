namespace WebMoney.Entities
{
    public class CompanyUser
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string PhoneNumber { get; set; }
        public string Walletnumber { get; set; }
    }
}
