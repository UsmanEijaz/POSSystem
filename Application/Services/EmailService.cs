using System.Net;
using System.Net.Mail;
using Domain.Interfaces;
using Infrastructure.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> options,
                       ILogger<EmailService> logger)
        {
            _settings = options.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(
       string to,
       string subject,
       string htmlBody,
       string attachmentPath = null)
        {
            try
            {
                var email = new MailMessage();

                email.From = new MailAddress(_settings.From);
                email.Subject = subject;
                email.To.Add(new MailAddress(to));

                if (!string.IsNullOrEmpty(attachmentPath) &&
                  File.Exists(attachmentPath))
                {
                    email.Attachments.Add(new Attachment(attachmentPath));
                }

                email.Body = htmlBody;
                email.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    smtp.Port = _settings.Port;
                    smtp.Host = _settings.Host;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(_settings.From, _settings.Password);
                    //smtp.Send(email);
                    await smtp.SendMailAsync(email);
                }

                _logger.LogInformation("Email sent successfully to {Email}", to);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Email sending failed");
                throw;
            }
        }
    }
}
