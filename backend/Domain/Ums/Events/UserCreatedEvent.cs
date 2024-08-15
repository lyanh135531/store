using Domain.Core;
using Domain.Ums.Entities;

namespace Domain.Ums.Events;

public class UserCreatedEvent(User user) : BaseEvent
{
    public User User { get; set; } = user;
}