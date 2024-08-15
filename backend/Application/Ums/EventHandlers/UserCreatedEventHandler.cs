using Application.Core.DTOs;
using Application.Core.Services;
using Application.Ums.DTOs;
using Domain.Ums.Events;
using MediatR;

namespace Application.Ums.EventHandlers;

public class UserCreatedEventHandler(IEmailService emailService) : INotificationHandler<UserCreatedEvent>
{
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var user = notification.User;

        var replaceEmailDto = new UserReplaceEmailDto
        {
            Name = user.FullName
        };
        await emailService.SendMail(new SendMailDto
        {
            To = user.Email,
            Name = user.UserName,
            Subject = "Welcome",
            TemplateName = "EmailTemplateCreateUser",
            ReplaceDto = replaceEmailDto
        });
    }
}