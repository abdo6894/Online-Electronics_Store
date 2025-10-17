using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.Identity
{
    public class CreateUser : BaseModel
    {
        public required string Fullname { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}
