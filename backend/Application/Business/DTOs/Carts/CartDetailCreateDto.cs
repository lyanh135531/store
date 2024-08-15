namespace Application.Business.DTOs.Carts;

public class CartDetailCreateDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}