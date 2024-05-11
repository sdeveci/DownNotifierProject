using DownNotifier.API.Entities;
using DownNotifier.API.Enums;
using System.Net.Mail;
using System.Net;

namespace DownNotifier.API.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;

        public NotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendNotification(TargetApp targetApp)
        {
            switch (targetApp.NotificationType)
            {
                case NotificationType.Email:
                    await SendEmailNotification(targetApp);
                    break;
                default:
                    throw new NotImplementedException($"Notification type '{targetApp.NotificationType}' is not supported.");
            }
        }

        private async Task SendEmailNotification(TargetApp targetApp)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            string fromAddress = smtpSettings["Username"];
            string toAddress = "sadikdeveci@gmail.com";
            string subject = $"[{targetApp.Name}] Application Down";
            string body = $"The target application '{targetApp.Name}' is down. URL: {targetApp.Url}";
            
            using (var message = new MailMessage(fromAddress, toAddress, subject, body))
            using (var client = new SmtpClient(smtpSettings["Server"]))
            {
                client.Port = Convert.ToInt32(smtpSettings["Port"]);
                client.Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]);
                client.EnableSsl = true;
                client.Send(message);
            }
        }
    }
}
