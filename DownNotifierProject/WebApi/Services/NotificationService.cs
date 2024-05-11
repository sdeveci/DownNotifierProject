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

        public async Task SendNotification(TargetApp pReq)
        {
            switch (pReq.NotificationType)
            {
                case NotificationType.Email:
                    await SendEmailNotification(pReq);
                    break;
                default:
                    throw new NotImplementedException($"Notification type '{pReq.NotificationType}' is not supported.");
            }
        }

        private async Task SendEmailNotification(TargetApp pReq)
        {
            var _smtpSettings = _configuration.GetSection("SmtpSettings");
            string fromAddress = _smtpSettings["Username"];
            string toAddress = "sadikdeveci@gmail.com";
            string subject = $"[{pReq.Name}] Application Down";
            string body = $"The target application '{pReq.Name}' is down. URL: {pReq.Url}";
            
            using (var message = new MailMessage(fromAddress, toAddress, subject, body))
            using (var client = new SmtpClient(_smtpSettings["Server"]))
            {
                client.Port = Convert.ToInt32(_smtpSettings["Port"]);
                client.Credentials = new NetworkCredential(_smtpSettings["Username"], _smtpSettings["Password"]);
                client.EnableSsl = true;
                client.Send(message);
            }
        }
    }
}
