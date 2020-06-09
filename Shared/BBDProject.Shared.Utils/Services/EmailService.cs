

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BBDProject.Shared.Models.Email;
using BBDProject.Shared.Models.User;
using BBDProject.Shared.Utils.Helpers;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Serilog;

namespace BBDProject.Shared.Utils.Services
{
    public sealed class EmailService : IEmailService
    {
        public ILogger Logger { get; set; }
        public EmailBuilder EmailBuilder { get; set; }

        private readonly EmailServiceOptions _options;
        public EmailService(IOptions<EmailServiceOptions> emailServiceOptions)
        {
            _options = emailServiceOptions.Value;
        }
        public async Task SendEmailAsync(CancellationToken cancellationToken, params EmailDefinition[] emailDefinitions)
        {
            if (emailDefinitions == null)
            {
                throw new ArgumentNullException(nameof(emailDefinitions));
            }
            var mimeMessages = new MimeMessage[emailDefinitions.Length];
            for (var i = 0; i < emailDefinitions.Length; i++)
            {
                var emailDefinition = emailDefinitions[i];
                var mimeMessage = new MimeMessage();
                foreach (var emailAdr in emailDefinition.To)
                {
                    mimeMessage.To.Add(new MailboxAddress(emailAdr.Name, emailAdr.Address));
                }
                mimeMessage.Subject = emailDefinition.Subject;
                var builder = new BodyBuilder();
                if (!string.IsNullOrWhiteSpace(emailDefinition.Body))
                {
                    builder.TextBody = emailDefinition.Body;
                }
                if (!string.IsNullOrWhiteSpace(emailDefinition.HtmlBody))
                {
                    builder.HtmlBody = emailDefinition.HtmlBody;
                }                
                mimeMessage.Body = builder.ToMessageBody();
                mimeMessage.From.Add(new MailboxAddress(_options.From, _options.FromAddress));
                mimeMessages[i] = mimeMessage;
            }
            await SendEmailAsyncInner(cancellationToken, mimeMessages);
        }

        public async Task SendConfirmAccountEmail(CancellationToken cancellationToken, string token, BaseUserInfo user)
        {
            await SendEmailAsync(cancellationToken, EmailBuilder.BuilConfirmAccountEmail(token, user));
        }

        public async Task SendResetPasswordEmail(CancellationToken cancellationToken, string token, BaseUserInfo user)
        {
            await SendEmailAsync(cancellationToken, EmailBuilder.BuildResetPasswordEmail(token, user));
        }

        private async Task SendEmailAsyncInner(CancellationToken cancellationToken,
            IEnumerable<MimeMessage> mimeMessages)
        {
            if (!string.IsNullOrWhiteSpace(_options.Server))
            {
                try
                {
                    using (var smtpClient = new SmtpClient())
                    {
                        if (_options.SecureSocketOptions == SecureSocketOptions.None)
                        {
                            await smtpClient.ConnectAsync(_options.Server, _options.Port, false, cancellationToken);
                        }
                        else
                        {
                            smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                            await smtpClient.ConnectAsync(_options.Server, _options.Port, _options.SecureSocketOptions, cancellationToken);
                        }
                        if (!string.IsNullOrWhiteSpace(_options.UserName))
                        {
                            await smtpClient.AuthenticateAsync(_options.UserName, _options.Password, cancellationToken);
                        }
                        foreach (var mimeMessage in mimeMessages)
                        {
                            await smtpClient.SendAsync(mimeMessage, cancellationToken);
                        }
                        await smtpClient.DisconnectAsync(true, cancellationToken);
                    }
                }
                catch (Exception e)
                {
                    Logger.Error("Error during sending mail message");
                    throw e;
                }
            }
            else
            {
                Logger.Warning("Email service not configured");
                throw new Exception("Email service not configured");
            }
        }
    }
}