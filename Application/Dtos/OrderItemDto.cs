using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class OrderItemDto
    {
        public string? ProductName { get; set; }
        public string? CategoryName { get; set; }
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal SubTotal => Quantity * UnitPrice;
    }

}
