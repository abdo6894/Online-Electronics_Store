﻿namespace BL.Dtos.RefreshTokenDtos
{
    public class RefreshTokenResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
