
using System.ComponentModel;

namespace WebMoney.Enums
{
    public enum TransactionType
    {
        [Description("Перевод")]
        Transfer = 0,
        [Description("Снятие")]
        Withdrawal = 1,
        [Description("Пополнение")]
        Refill = 2,
        [Description("Оплата услуг")]
        PaymentForServices = 3
    }
}
