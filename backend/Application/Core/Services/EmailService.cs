using Application.Configs;
using Application.Core.DTOs;
using Application.Core.Extensions;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Mjml.Net;

namespace Application.Core.Services;

public class EmailService(
    IHostEnvironment hostEnvironment,
    MjmlRenderer mjmlRenderer,
    IOptions<SmtpSettings> options)
    : IEmailService
{
    private readonly SmtpSettings _smtpSettings = options.Value;

    public async Task SendMail(SendMailDto sendMailDto)
    {
        var rootPath = hostEnvironment.ContentRootPath;
        var template = await File.ReadAllTextAsync($"{rootPath}/Templates/{sendMailDto.TemplateName}.mjml");
        var email = template.ReplaceParams(sendMailDto.ReplaceDto);

        var mjmlRenderResult = mjmlRenderer.Render(email);
        if (!mjmlRenderResult.Errors.Any())
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
            message.To.Add(new MailboxAddress(sendMailDto.Name ?? string.Empty, sendMailDto.To));
            message.Subject = sendMailDto.Subject;

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = mjmlRenderResult.Html
            };

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port,
                    SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_smtpSettings.ApiKey, _smtpSettings.ApiSecretKey);
                await client.SendAsync(message);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}