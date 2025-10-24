using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
        public class Order : BaseEntity
    {

            public Guid? UserId { get; set; }
            public virtual AppUser? User { get; set; }

            public decimal TotalAmount { get; set; }

            public virtual ICollection<OrderItem>? OrderItems { get; set; }
            public string Status { get; set; } = "Pending"; // Pending, Shipped, Completed, Cancelled
        }
    }
