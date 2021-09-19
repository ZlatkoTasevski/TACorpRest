using Microsoft.EntityFrameworkCore;
using Tasevski.Services.Email.DbContexts;
using Tasevski.Services.Email.Messages;
using Tasevski.Services.Email.Models;

namespace Tasevski.Services.Email.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContext;
        public EmailRepository(DbContextOptions<ApplicationDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SendAndLogEmail(UpdatePaymentResultMessage message)
        {
            EmailLog emailLog = new EmailLog()
            {
                Email = message.Email,
                EmailSent = DateTime.Now,
                Log = $"Нарачката - {message.OrderId} е креирана успешно!"
            };

            await using var _db = new ApplicationDbContext(_dbContext);
            _db.EmailLogs.Add(emailLog);
            await _db.SaveChangesAsync();
        }
    }
}
