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

        public void SendEmailAsync(string name, string phone, string message, string emailFrom, string passwordFrom)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(name, emailFrom));
            emailMessage.To.Add(new MailboxAddress("", "yakovenko182003@gmail.com"));
            emailMessage.Subject = "Сообщение пользователя - " + emailFrom;
            emailMessage.Body = new TextPart("Plain")
            {
                Text = message + "\n\nТелефон пользователя: " +  phone
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect("smtp.gmail.com", 25, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(emailFrom, passwordFrom);
                    client.Send(emailMessage);
                    
                    client.Disconnect(true);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
