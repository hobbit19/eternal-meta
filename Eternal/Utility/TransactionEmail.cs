using Eternal.Models;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;

namespace Eternal.Utility
{
    public class TransactionEmail
    {
        public static async Task SendVerificationEmail(User user)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Eternal Meta", "eternal.meta.transactions@gmail.com"));
            message.To.Add(new MailboxAddress(user.Username, user.Email));
            message.Subject = "Eternal Meta - Registration";

            var builder = new BodyBuilder
            {
                HtmlBody = "<a href='http://Localhost:49288/Users/Activate?userId=" + user.UserID + "&token=" + user.Token + "'>Activate My Account</a>"
            };

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("eternal.meta.transactions@gmail.com", "pa$$w0rd");

                client.Send(message);

                client.Disconnect(true);
            }
        }

        public static void SendRecoveryEmail(User user)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Eternal Meta", "eternal.meta.transactions@gmail.com"));
            message.To.Add(new MailboxAddress(user.Username, user.Email));
            message.Subject = "Eternal Meta - Reset Password";

            var builder = new BodyBuilder
            {
                HtmlBody = "<a href='http://Localhost:49288/Users/ResetPassword?userId=" + user.UserID + "&token=" + user.Token + "'>Reset Password</a>"
            };

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("eternal.meta.transactions@gmail.com", "pa$$w0rd");

                client.Send(message);

                client.Disconnect(true);
            }
        }
    }
}
