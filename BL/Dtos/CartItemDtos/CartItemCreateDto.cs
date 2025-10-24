using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos.CartItemDtos
{
    public class CartItemCreateDto : BaseDto
    {

        public int Quantity { get; set; }
    }
}
