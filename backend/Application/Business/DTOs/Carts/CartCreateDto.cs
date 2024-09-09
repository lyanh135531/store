namespace Application.Business.DTOs.Carts;

public class CartCreateDto
{
    public List<CartDetailCreateDto> CartDetails { get; set; } = [];
}