
using Application.Dtos;
using Application.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Electronic_Store.Models;

namespace Online_Electronic_Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        #region Fields
        private readonly IProductService _ProductService;
        private readonly ILogger<ProductsController> _logger;
        #endregion

        #region Constructor
        public ProductsController(IProductService ProductService, ILogger<ProductsController> logger)
        {
            _ProductService = ProductService;
            _logger = logger;
        }

        #endregion

        #region EndPoints
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all Products");
                var Products = await _ProductService.GetProductsWithCategory();
                return Ok(ApiResponse<List<ProductDto>>.SuccessResponse(Products));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching Product");
                return StatusCode(500, ApiResponse<string>.FailResponse("Internal server error"));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                _logger.LogInformation($"Fetching Product {id}");
                var Product = await _ProductService.GetProductWithCategoryById(id);
                if (Product == null)
                    return NotFound(ApiResponse<ProductDto>.FailResponse("Product not found"));

                return Ok(ApiResponse<ProductDto>.SuccessResponse(Product));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching Product {id}");
                return StatusCode(500, ApiResponse<string>.FailResponse("Internal server error"));
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ProductDto dto)
        {
            try
            {
                _logger.LogInformation("Creating new Product");
                var result = await _ProductService.Add(dto);
                if (!result)
                    return BadRequest(ApiResponse<string>.FailResponse("Failed to create Product"));

                return Ok(ApiResponse<string>.SuccessResponse("Product created successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Product");
                return StatusCode(500, ApiResponse<string>.FailResponse("Internal server error"));
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductDto dto)
        {
            try
            {
                dto.Id = id;
                _logger.LogInformation($"Updating Product {id}");
                var result = await _ProductService.Update(dto);
                if (!result)
                    return BadRequest(ApiResponse<string>.FailResponse("Failed to update Product"));

                return Ok(ApiResponse<string>.SuccessResponse("Product updated successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating Product {id}");
                return StatusCode(500, ApiResponse<string>.FailResponse("Internal server error"));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                _logger.LogInformation($"Deleting Product {id}");
                var result = await _ProductService.Delete(id);
                if (!result)
                    return BadRequest(ApiResponse<string>.FailResponse("Failed to delete Product"));

                return Ok(ApiResponse<string>.SuccessResponse("Product deleted successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting Product {id}");
                return StatusCode(500, ApiResponse<string>.FailResponse("Internal server error"));
            }
        } 
        #endregion


    }
}

