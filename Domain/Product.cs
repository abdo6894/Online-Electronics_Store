using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        public string? ImageMain { get; set; } = null!;
        public string? ImageSecond { get; set; }
        public string? ImageThird { get; set; }

        public int Quantity { get; set; }

        public Category? Category { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ICollection<CartItem>? CartItems { get; set; }

    }
}
