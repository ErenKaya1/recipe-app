using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using RecipeApp.Core.Models.Mail;
using RecipeApp.Service.Contracts;

namespace RecipeApp.Service.Services
{
    public class MailService : IMailService
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public async Task SendAsync(MailDTO data)
        {
            if (string.IsNullOrEmpty(Host)) throw new ArgumentNullException(Host);
            if (string.IsNullOrEmpty(Username)) throw new ArgumentNullException(Username);
            if (string.IsNullOrEmpty(Password)) throw new ArgumentNullException(Password);

            using (var client = new SmtpClient())
            {
                client.Host = Host;
                client.Port = Port;
                client.EnableSsl = UseSsl;
                client.Credentials = new NetworkCredential(Username, Password);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(data.From),
                    Subject = data.Subject,
                    Body = data.Content,
                    IsBodyHtml = true
                };

                foreach (var to in data.To)
                    mailMessage.To.Add(new MailAddress(to));

                try
                {
                    await client.SendMailAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}