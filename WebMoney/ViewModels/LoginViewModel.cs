using System.ComponentModel.DataAnnotations;

namespace WebMoney.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Уникальный код")]
        public string UniqueСode { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }

    }
}
