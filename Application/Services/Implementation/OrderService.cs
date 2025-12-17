using Application.Dtos;
using Application.Mapping;
using Application.Services.Implementation.Generic;
using Application.Services.Interfaces;
using Domain;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services.Implementation
{
    public class OrderService : GenericService<Order, OrderDto>, IOrderService
    {
      private readonly  IOrderRepository _orderRepository;
        private readonly IMappingService _mapper;
        private readonly ICartItemService _cartService;
        private readonly IProductService _productService;
        private readonly IOrderItemRepository _orderItemRepository;
        public OrderService(
          IOrderRepository OrderRepo,
           IOrderItemRepository orderItemRepository,
           ICartItemService cartService,
           IProductService productService,
           IMappingService mapper ) : base(OrderRepo, mapper)
        {
            _orderRepository = OrderRepo;
            _productService = productService;
            _cartService = cartService;
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;

        }

        public async Task<bool> CreateOrderAsync(Guid userId)
        {
            // Get user's cart items
            var cartItems = (await _cartService.GetAll())
                            .Where(c => c.UserId == userId)
                            .ToList();

            if (!cartItems.Any())
                return false;

            decimal totalAmount = 0;

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                TotalAmount = 0,
                CreatedDate = DateTime.UtcNow,
                Status = "Pending"
            };

            await _orderRepository.Add(order);

            foreach (var ci in cartItems)
            {
                var product = await _productService.GetById(ci.ProductId);
                if (product == null)
                    continue;

                var orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,  
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    UnitPrice = product.Price
                };

                totalAmount += product.Price * ci.Quantity;

                await _orderItemRepository.Add(orderItem);
            }

            order.TotalAmount = totalAmount;
            await _orderRepository.Update(order);


            foreach (var ci in cartItems)
                await _cartService.Delete(ci.Id);

            return true;
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();

            var result = orders.Select(o => new OrderDto
            {
                TotalAmount = o.TotalAmount,
                CreatedDate = o.CreatedDate,
                Status = o.Status,
                Items = o.Items!.Select(i => new OrderItemDto
                {
                    ProductName = i.Product!.Name,
                    CategoryName = i.Product.Category!.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            }).ToList();

            return result;
        }

        public async Task<List<OrderDto>> GetUserOrdersAsync(Guid userId)
        {
            var orders = await _orderRepository.GetUserOrdersAsync(userId);

            var result = orders.Select(o => new OrderDto
            {
                TotalAmount = o.TotalAmount,
                CreatedDate = o.CreatedDate,
                Status = o.Status,
                Items = o.Items!.Select(i => new OrderItemDto
                {
                    ProductName = i.Product!.Name,
                    CategoryName = i.Product!.Category!.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            }).ToList();

            return result;
        }
    }
}

