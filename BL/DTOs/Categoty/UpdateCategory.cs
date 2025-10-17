using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Categoty
{
    public class UpdateCategory : CategoryBase
    {
        [Required]
        public Guid Id { get; set; }
    }
}
