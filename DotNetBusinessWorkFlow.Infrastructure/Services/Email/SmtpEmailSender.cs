using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace DotNetBusinessWorkFlow.Infrastructure.Services.Email;

public class SmtpEmailSender : IEmailSender
{
    private readonly EmailSettings _settings;

    public SmtpEmailSender(IOptions<EmailSettings> options)
    {
        _settings = options.Value;
    }

    public async Task SendAsync(
        string to,
        string subject,
        string body,
        byte[] attachment,
        string attachmentName)
    {
        using var client = new SmtpClient(_settings.SmtpHost, _settings.Port)
        {
            Credentials = new NetworkCredential(
                _settings.Username,
                _settings.Password),
            EnableSsl = true
        };

        using var message = new MailMessage(
            _settings.From,
            to,
            subject,
            body);

        message.Attachments.Add(
            new Attachment(new MemoryStream(attachment), attachmentName));

        await client.SendMailAsync(message);
    }
}
