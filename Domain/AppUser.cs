using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Fullname { get; set; } = string.Empty;

        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<CartItem>? CartItems { get; set; }
    }
}
