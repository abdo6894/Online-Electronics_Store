using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ProductDto : BaseDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; } = true;
        public Guid CategoryId { get; set; }

    }
}
