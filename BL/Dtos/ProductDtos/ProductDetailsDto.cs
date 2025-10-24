namespace BL.Dtos.ProductDtos
{
    public class ProductDetailsDto : BaseDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
