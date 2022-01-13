using Microsoft.AspNetCore.Identity;

namespace WebMoney.Entities
{
    public class User : IdentityUser
    {
        public string UniqueСode { get; set; }
    }   
}
