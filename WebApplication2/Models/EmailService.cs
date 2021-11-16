using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class EmailService
    {
        public void SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "yakovenko182003@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", "yakovenko182003@gmail.com"));
            emailMessage.Subject = "мддаа";
            emailMessage.Body = new TextPart("Plain")
            {
                Text = "проверка"
            };

            using (var client = new SmtpClient())
            {
                 client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                 client.Connect("smtp.gmail.com", 25, false);
                 client.AuthenticationMechanisms.Remove("XOAUTH2");
                 client.Authenticate("email", "parol");
                 client.Send(emailMessage);

                 client.Disconnect(true);
            }
        }
    }
}
