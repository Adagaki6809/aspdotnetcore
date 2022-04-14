using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using MailKit.Security;
using Site.Utilities;

namespace Site
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            var dbConfigDictionary = DBConfigHelper.GetDBConfig();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", dbConfigDictionary["adminEmail"]));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTlsWhenAvailable);
                string userName = dbConfigDictionary["adminEmail"]; // вообще это почта
                string password = dbConfigDictionary["applicationPassword"]; // пароль приложения гугл, нужен для доп безопасности, без него не пускает
                await client.AuthenticateAsync(userName, password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}