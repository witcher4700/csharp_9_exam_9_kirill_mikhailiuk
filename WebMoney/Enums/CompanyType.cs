using System.ComponentModel;

namespace WebMoney.Enums
{
    public enum CompanyType
    {
        [Description("Бытовые услуги")]
        DomesticServices = 0,
        [Description("Телефонный оператор")]
        PhoneOperator = 1,
        [Description("Интернет провайдер")]
        InternetProvider = 2
    }
}
