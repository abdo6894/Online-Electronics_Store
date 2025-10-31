
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
    public class CartController : ControllerBase
    {
        #region Fields
        private readonly IGenericService<CartItem, CartItemDto> _cartService;
        private readonly ILogger<CartController> _logger;

        #endregion

        #region Constructor
        public CartController(IGenericService<CartItem, CartItemDto> cartService,
                            ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }
        #endregion

        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var userId = GetUserIdFromClaims();
                if (userId == null)
                    return Unauthorized(ApiResponse<string>.FailResponse("Invalid user token"));

                var cartItems = (await _cartService.GetAll())
                                .Where(c => c.UserId == userId)
                                .ToList();

                return Ok(ApiResponse<List<CartItemDto>>.SuccessResponse(cartItems));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching cart items");
                return StatusCode(500, ApiResponse<string>.FailResponse("Internal server error"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CartItemDto dto)
        {
            try
            {
                var userId = GetUserIdFromClaims();
                if (userId == null)
                    return Unauthorized(ApiResponse<string>.FailResponse("Invalid user token"));

                // تأكد إن المنتج موجود قبل الإضافة لو عندك ProductService
                dto.UserId = userId;

                var result = await _cartService.Add(dto);
                if (!result)
                    return BadRequest(ApiResponse<string>.FailResponse("Failed to add item to cart"));

                return Ok(ApiResponse<string>.SuccessResponse("Item added to cart"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding cart item");
                return StatusCode(500, ApiResponse<string>.FailResponse("Internal server error"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CartItemDto dto)
        {
            try
            {
                dto.Id = id;
                var result = await Task.Run(() => _cartService.Update(dto));
                if (!result) return BadRequest(ApiResponse<string>.FailResponse("Failed to update cart item"));

                return Ok(ApiResponse<string>.SuccessResponse("Cart item updated"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating cart item {id}");
                return StatusCode(500, ApiResponse<string>.FailResponse("Internal server error"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await Task.Run(() => _cartService.Delete(id));
                if (!result) return BadRequest(ApiResponse<string>.FailResponse("Failed to delete cart item"));

                return Ok(ApiResponse<string>.SuccessResponse("Cart item deleted"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting cart item {id}");
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



