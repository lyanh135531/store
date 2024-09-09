using Application.Configs;
using Application.Core.DTOs;
using Application.Core.Extensions;
using Domain.Emails.Entities;
using Domain.Emails.Repositories;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Mjml.Net;

namespace Application.Core.Services;

public class EmailService(
    IHostEnvironment hostEnvironment,
    MjmlRenderer mjmlRenderer,
    IOptions<SmtpSettings> options,
    IEmailMessageRepository emailMessageRepository,
    ILogger<EmailService> logger)
    : IEmailService
{
    private readonly SmtpSettings _smtpSettings = options.Value;

    public async Task SendMail(SendMailDto sendMailDto)
    {
        var rootPath = hostEnvironment.ContentRootPath;
        var template = await File.ReadAllTextAsync($"{rootPath}/Templates/{sendMailDto.TemplateName}.mjml");
        if (sendMailDto.ReplaceDto == null) return;
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

            var emailMessage = await emailMessageRepository.AddAsync(new EmailMessage()
            {
                Subject = sendMailDto.Subject,
                Status = EmailStatus.Sending,
                FromEmail = _smtpSettings.SenderEmail,
                MjmlContent = template,
                HtmlContent = mjmlRenderResult.Html,
                ToEmail = sendMailDto.To,
            }, true);

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port,
                    SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_smtpSettings.ApiKey, _smtpSettings.ApiSecretKey);
                await client.SendAsync(message);
            }
            catch (Exception ex)
            {
                emailMessage.Status = EmailStatus.Error;
                await emailMessageRepository.UpdateAsync(emailMessage, true);
                logger.LogError(ex.Message);
            }
            finally
            {
                emailMessage.Status = EmailStatus.Success;
                await emailMessageRepository.UpdateAsync(emailMessage, true);
                await client.DisconnectAsync(true);
            }
        }
    }
}