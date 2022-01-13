namespace WebMoney.Entities
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string UniqueNumber { get; set; }
        public string UserId { get; set; }
        public double MoneyCount { get; set; }
    }
}
