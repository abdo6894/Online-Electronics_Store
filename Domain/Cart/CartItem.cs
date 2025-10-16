using System.ComponentModel.DataAnnotations;

namespace Domain.Cart
{
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string? UserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
