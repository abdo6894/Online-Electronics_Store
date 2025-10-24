namespace BL.Dtos.ProductDtos
{
    public class ProductListDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }

        // ممكن تضيف اسم الكاتيجوري بدل الـ Id عشان العرض
        public string? CategoryName { get; set; }
    }
}
