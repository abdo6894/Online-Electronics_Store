using Application.Dtos;
using Application.Services.Interfaces;
using Application.Services.Interfaces.Generic;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Electronic_Store.Models;
using System.Security.Claims;

namespace Online_Electronic_Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        #region Fields
        private readonly IGenericService<Order, OrderDto> _orderService;
        private readonly IProductService _productService;
        private readonly IGenericService<OrderItem, OrderItemDto> _orderItemService;
        private readonly IGenericService<CartItem, CartItemDto> _cartService;
        private readonly ILogger<OrdersController> _logger;
        #endregion

        #region Constructor
        public OrdersController(
          IGenericService<Order, OrderDto> orderService,
          IGenericService<OrderItem, OrderItemDto> orderItemService,
          IGenericService<CartItem, CartItemDto> cartService,
           IProductService productService,
          ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _cartService = cartService;
            _logger = logger;
            _productService = productService;
        }
        #endregion

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder()
        {
            try
            {
                var userId = GetUserIdFromClaims();
                if (userId == null)
                    return Unauthorized(ApiResponse<string>.FailResponse("Invalid user token"));

                var cartItems = (await _cartService.GetAll())
                                .Where(c => c.UserId == userId)
                                .ToList();

                if (!cartItems.Any())
                    return BadRequest(ApiResponse<string>.FailResponse("Cart is empty"));

                decimal totalAmount = 0;
                var orderItemsDtos = new List<OrderItemDto>();

                foreach (var ci in cartItems)
                {
                    var product = await _productService.GetById(ci.ProductId);
                    if (product == null)
                        return BadRequest(ApiResponse<string>.FailResponse($"Product {ci.ProductId} not found"));

                    totalAmount += product.Price * ci.Quantity;

                    orderItemsDtos.Add(new OrderItemDto
                    {
                        ProductId = ci.ProductId,
                        Quantity = ci.Quantity,
                        UnitPrice = product.Price
                    });
                }

                var orderDto = new OrderDto
                {
                    UserId = userId,
                    TotalAmount = totalAmount,
                    Status = "Pending"
                };

                var addedOrder = await _orderService.Add(orderDto);
                if (!addedOrder)
                    return BadRequest(ApiResponse<string>.FailResponse("Failed to create order"));

                // جلب آخر Order مضاف
                var createdOrder = (await _orderService.GetAll()).Last();

                // إضافة OrderItems
                foreach (var oi in orderItemsDtos)
                {
                    oi.OrderId = createdOrder.Id;
                    await _orderItemService.Add(oi);
                }

                // مسح العناصر من Cart
                foreach (var ci in cartItems)
                    await _cartService.Delete(ci.Id);

                return Ok(ApiResponse<string>.SuccessResponse($"Order {createdOrder.Id} created successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order");
                return StatusCode(500, ApiResponse<string>.FailResponse("Internal server error"));
            }
        }

        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            try
            {
                var userId = GetUserIdFromClaims();
                if (userId == null)
                    return Unauthorized(ApiResponse<string>.FailResponse("Invalid user token"));

                var orders = (await _orderService.GetAll())
                             .Where(o => o.UserId == userId)
                             .ToList();

                return Ok(ApiResponse<List<OrderDto>>.SuccessResponse(orders));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user orders");
                return StatusCode(500, ApiResponse<string>.FailResponse("Internal server error"));
            }
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAll();
                return Ok(ApiResponse<List<OrderDto>>.SuccessResponse(orders));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all orders");
                return StatusCode(500, ApiResponse<string>.FailResponse("Internal server error"));
            }
        }
        private Guid? GetUserIdFromClaims()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (Guid.TryParse(userIdClaim, out var userId))
                return userId;
            return null;
        }

    }


}
