namespace BL.Dtos.ProductDtos
{
    public class ProductUpdateDto : BaseDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }
        public Guid CategoryId { get; set; }
    }
}
