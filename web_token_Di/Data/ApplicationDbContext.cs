using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using web_token_Di.Models;
using web_token_Di.Models.DTOs;
using web_token_Di.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace web_token_Di.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly Guid _currentTenantId;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantService tenantService) 
            : base(options)
        {
            _currentTenantId = tenantService.GetTenantId();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<EmployeeModel>()
                .HasQueryFilter(e => e.TenantId == _currentTenantId);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.TenantId = _currentTenantId;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<EmployeeModel> Employee { get; set; }
    }
}
