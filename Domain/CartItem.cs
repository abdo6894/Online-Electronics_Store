using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class CartItem : BaseEntity
    {
        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public int Quantity { get; set; }
        public Guid? UserId { get; set; }
        public virtual AppUser? User { get; set; }
    }
}
