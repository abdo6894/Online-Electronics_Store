using BL.Dtos.OrderItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos.OrderDtos
{
    public class OrderCreateDto : BaseDto
    {
        public List<OrderItemCreateDto> OrderItems { get; set; } = new();
    }
}
