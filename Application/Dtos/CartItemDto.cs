using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CartItemDto : BaseDto
    {

        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Guid? UserId { get; set; }
    }
}
