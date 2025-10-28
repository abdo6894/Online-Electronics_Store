using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos.AppUserDtos
{
    public class AuthResultDto
    {
        public bool Success { get; set; }
        public string ?Token { get; set; }
        public string? Error { get; set; }
        public UserDto? User { get; set; }

    }
}
