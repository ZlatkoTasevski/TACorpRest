using Tasevski.Services.Email.Messages;
using Tasevski.Services.Email.Models;

namespace Tasevski.Services.Email.Repository
{
    public interface IEmailRepository
    {
        Task SendAndLogEmail(UpdatePaymentResultMessage message);
    }
}
