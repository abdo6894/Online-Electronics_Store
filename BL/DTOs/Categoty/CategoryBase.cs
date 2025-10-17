using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Categoty
{
    public class CategoryBase
    {
        [Required]
        public string? Name { get; set; }
    }
}
