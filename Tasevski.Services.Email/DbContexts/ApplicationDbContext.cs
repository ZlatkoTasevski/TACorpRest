using Microsoft.EntityFrameworkCore;
using Tasevski.Services.Email.Models;

namespace Tasevski.Services.Email.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<EmailLog> EmailLogs { get; set; }
    }
}
