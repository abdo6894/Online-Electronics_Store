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
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IOrderItemService _orderItemService;
        private readonly ICartItemService _cartService;
        private readonly ILogger<OrdersController> _logger;
        #endregion

        #region Constructor
        public OrdersController(
          IOrderService orderService,
          IOrderItemService orderItemService,
          ICartItemService cartService,
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

        #region EndPoints
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder()
        {
            try
            {
                var userId = GetUserIdFromClaims();
                if (userId == null)
                    return Unauthorized(ApiResponse<string>.FailResponse("Invalid user token"));

                var result = await _orderService.CreateOrderAsync(userId.Value);
                if (!result)
                    return BadRequest(ApiResponse<string>.FailResponse("Failed to create order"));

                return Ok(ApiResponse<string>.SuccessResponse("Order created successfully"));
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

                var orders = await _orderService.GetUserOrdersAsync(userId.Value);
                return Ok(ApiResponse<List<OrderDto>>.SuccessResponse(orders));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user orders");
                return StatusCode(500, ApiResponse<string>.FailResponse("Internal server error"));
            }
        }

        [HttpGet("All-Order")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
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

        #endregion
    }
}
