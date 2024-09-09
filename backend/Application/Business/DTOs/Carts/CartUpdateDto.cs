namespace Application.Business.DTOs.Carts;

public class CartUpdateDto
{
    public List<CartDetailUpdateDto> CartDetails { get; set; } = [];
}