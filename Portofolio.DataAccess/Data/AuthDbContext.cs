using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.Data
{
    public class AuthDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AuthDbContext(DbContextOptions Options, UserManager<IdentityUser> userManager) : base(Options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUser>()
                .HasData(
                new IdentityUser { Email = "mohamed@gmail.com", UserName = "mooo", PasswordHash = "11"})
        }
    }
}
