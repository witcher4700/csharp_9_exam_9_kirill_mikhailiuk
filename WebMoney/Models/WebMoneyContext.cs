using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMoney.Models
{

    public class WebMoneyContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ServiceCompany> ServiceCompanies { get; set; }
        public DbSet<CompanyUser> CompanyUsers { get; set; }
        public WebMoneyContext(DbContextOptions<WebMoneyContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceCompany>().HasData(
            new { Id = 1, Name = "Megacom", CompanyType= CompanyType.ТелефонныйОператор },
            new { Id = 2, Name = "Aknet", CompanyType = CompanyType.ИнтернетОператор},
            new { Id = 3, Name = "Svet", CompanyType = CompanyType.БытовыеУслуги},
            new { Id = 4, Name = "Gas", CompanyType = CompanyType.БытовыеУслуги},
            new { Id = 5, Name = "O!", CompanyType = CompanyType.ТелефонныйОператор}
            );
            modelBuilder.Entity<BankAccount>().HasData(
            new { Id = 2, UniqueNumber = "MEGACOM11111", UserId = "1",MoneyCount = 0.0 },
            new { Id = 3, UniqueNumber = "AKNET2222222", UserId = "2", MoneyCount = 0.0 },
            new { Id = 4, UniqueNumber = "SVET33333333", UserId = "3", MoneyCount = 0.0 },
            new { Id = 5, UniqueNumber = "GAS444444444", UserId = "4", MoneyCount = 0.0 },
            new { Id = 6, UniqueNumber = "O!5555555555", UserId = "5", MoneyCount = 0.0 }
            );
            modelBuilder.Entity<CompanyUser>().HasData(
            new { Id = 1, Walletnumber = "MEGACOM11111", PhoneNumber = "996551222770", CompanyId = 1 },
            new { Id = 2, Walletnumber = "AKNET2222222", PhoneNumber = "996551222770", CompanyId = 2 },
            new { Id = 3, Walletnumber = "SVET33333333", PhoneNumber = "996551222770", CompanyId = 3 },
            new { Id = 4, Walletnumber = "GAS444444444", PhoneNumber = "996551222770", CompanyId = 4 },
            new { Id = 5, Walletnumber = "O!5555555555", PhoneNumber = "996551222770", CompanyId = 5 }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
