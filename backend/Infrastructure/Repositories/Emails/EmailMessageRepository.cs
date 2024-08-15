using Domain.Emails.Entities;
using Domain.Emails.Repositories;
using Infrastructure.Core;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Emails;

public class EmailMessageRepository(ApplicationDbContext applicationDbContext)
    : EfCoreRepository<EmailMessage, Guid>(applicationDbContext), IEmailMessageRepository
{
}