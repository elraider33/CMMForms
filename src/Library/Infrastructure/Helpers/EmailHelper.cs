using Library.Application.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;

namespace Library.Infrastructure.Helpers
{
    public class EmailConfig 
    { 
        public string From { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
    }
    public class EmailHelper : IEmailHelper
    {
        private readonly EmailConfig emailConfig;
        private readonly ILogger<EmailHelper> logger;

        public EmailHelper(IOptions<EmailConfig> options, ILogger<EmailHelper> logger)
        {
            this.emailConfig = options.Value;
            this.logger = logger;
        }

        public void SendEmail(string body)
        {
            this.logger.LogInformation("Sending Email");
            try
            {
                using var message = new MailMessage();
                using var smtp = new SmtpClient();
                message.From = new MailAddress(emailConfig.From);
                message.To.Add(new MailAddress("alonso@neosourcestudios.com"));
                message.Subject = "CMM Error";
                message.IsBodyHtml = true;
                message.Body = body;
                smtp.Port = emailConfig.Port;
                smtp.Host = emailConfig.Host;
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(emailConfig.From, emailConfig.Password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

            }
            catch (Exception e)
            {
                logger.LogInformation("Error sending Email");
                logger.LogError(e.Message, e);
           
            }
        }
    }
}
