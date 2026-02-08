using Domain.Interfaces;
using Infrastructure.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

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
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress(_settings.DisplayName, _settings.From));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;

                var builder = new BodyBuilder
                {
                    HtmlBody = htmlBody
                };

                if (!string.IsNullOrEmpty(attachmentPath) &&
                    File.Exists(attachmentPath))
                {
                    builder.Attachments.Add(attachmentPath);
                }

                email.Body = builder.ToMessageBody();
                
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(
                    _settings.Host,
                    _settings.Port,
                    SecureSocketOptions.StartTls);

                await smtp.AuthenticateAsync(
                    _settings.Username,
                    _settings.Password);

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

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
