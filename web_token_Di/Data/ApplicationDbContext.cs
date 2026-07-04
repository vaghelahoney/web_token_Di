using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using web_token_Di.Models;
using web_token_Di.Models.DTOs;

namespace web_token_Di.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

      public DbSet<EmployeeModel> Employee { get; set; }

    }
}
