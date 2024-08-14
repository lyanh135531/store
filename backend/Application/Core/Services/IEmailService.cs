using Application.Core.DTOs;

namespace Application.Core.Services;

public interface IEmailService
{
    Task SendMail(SendMailDto sendMailDto);
}