using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; } = true;

        public virtual Category? Category { get; set; }
        public Guid CategoryId { get; set; }
        public virtual ICollection<CartItem>? CartItems { get; set; }

    }
}
