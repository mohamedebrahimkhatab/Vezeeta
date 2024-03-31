using Vezeeta.Services.EmailService;

namespace Vezeeta.Services.Interfaces;

public interface IEmailSender
{
    void SendEmail(Message message);
    Task SendEmailAsync(Message message);
}
