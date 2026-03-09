using ReiDoChopp.Domain.Emails.Repositories.Models;

namespace ReiDoChopp.Domain.Emails.Repositories
{
    public interface IEmailsRepository
    {
        Task SendEmailAsync(EmailSendCommand emailSendModel);
    }
}
