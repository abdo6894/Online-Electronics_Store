﻿namespace BL.Dtos.OrderItemDtos
{
    public class OrderItemDetailsDto : BaseDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
    }
}
