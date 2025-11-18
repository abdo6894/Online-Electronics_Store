using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class OrderDto
    {
        public decimal TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; } = "Pending";

        public List<OrderItemDto> Items { get; set; } = new();
    }


}
