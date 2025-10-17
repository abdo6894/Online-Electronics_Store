using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Cart
{
    public class CreateAchieve
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string? UserId { get; set; }
        [Required]
        public DateTime CreatedData { get; set; }
    }
}
