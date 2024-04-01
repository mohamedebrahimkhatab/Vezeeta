namespace Vezeeta.Services.Utilities.EmailService;

public interface IEmailSender
{
    void SendEmail(Message message);
    Task SendEmailAsync(Message message);
}
