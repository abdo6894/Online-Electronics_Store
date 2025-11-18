using Domain;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetUserOrdersAsync(Guid userId);
        Task<List<Order>> GetAllOrdersAsync();
    }
}




