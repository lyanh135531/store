namespace Domain.Core;

public class Entity<TKey> : IEntity<TKey>
{
    public TKey Id { get; set; }
}