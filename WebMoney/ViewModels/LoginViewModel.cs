using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
