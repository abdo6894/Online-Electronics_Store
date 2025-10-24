﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos.RefreshTokenDtos
{
    public class RefreshTokenCreateDto : BaseDto
    {
        public Guid UserId { get; set; } 
        public string Token { get; set; } = string.Empty;
    }
}
