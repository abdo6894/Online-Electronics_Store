namespace Application.Dtos
{
    public class AddOrderDto
    {
        public List<AddOrderItemDto> Items { get; set; } = new();
    }

}
