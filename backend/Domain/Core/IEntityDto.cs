namespace Domain.Core;

public interface IEntityDto<TKey>
{
    TKey Id { get; set; }
}