using PlaylistManager.Logger;
using PlaylistManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistManager.Services
{
    public class EmailSendingService
    {
        private ILog logger = Logger.Logger.GetInstance;
        private string confirmationEmailUrl = "http://localhost:55766/Account/ValidateEmail";
       
        public async Task SendConfirmationEmailAsync(User user)
        {
            string callbackUrl = $"{confirmationEmailUrl}?userId={user.Id}";
            string link = $"<a href='{ callbackUrl}'>here</a>!";
            await SendEmailAsync(user.Email, "PaylistManager sign up", $"To confirm your account click  -> {link}");
        }

        private async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.mailtrap.io", 2525)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("24c543802de67b", "48ae66afff551a")
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("whoever@me.com")
                };
                mailMessage.To.Add(email);
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = subject;
                client.EnableSsl = true;
                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                logger.LogCustomException(ex, null);
                throw new ApplicationException($"Unable to load : '{ex.Message}'.");
            }
        }
    }
}
