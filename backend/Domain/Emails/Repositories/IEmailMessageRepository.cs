using Domain.Core;
using Domain.Emails.Entities;

namespace Domain.Emails.Repositories;

public interface IEmailMessageRepository : IRepository<EmailMessage, Guid>
{
}