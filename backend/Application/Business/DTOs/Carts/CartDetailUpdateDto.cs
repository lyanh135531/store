using Domain.Core;

namespace Application.Business.DTOs.Carts;

public class CartDetailUpdateDto : EntityDto<Guid>
{
    public int Quantity { get; set; }
}