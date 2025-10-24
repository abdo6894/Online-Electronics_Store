using BL.Dtos.OrderItemDtos;

namespace BL.Dtos.OrderDtos
{
    public class OrderDetailsDto : BaseDto
    {
        public Guid UserId { get; set; } 
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<OrderItemDetailsDto> OrderItems { get; set; } = new();
    }
}
