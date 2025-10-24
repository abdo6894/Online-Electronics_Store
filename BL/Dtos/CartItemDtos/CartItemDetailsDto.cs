namespace BL.Dtos.CartItemDtos
{
    public class CartItemDetailsDto : BaseDto
    {

        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
