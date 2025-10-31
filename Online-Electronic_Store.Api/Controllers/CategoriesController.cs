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
    [Authorize(Roles = "Admin")]
    public class CategoriesController : ControllerBase
    {
        #region Fields
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;

        #endregion

        #region Constructor
        public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        #endregion

        #region EndPoints

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all categories");
                var categories = await _categoryService.GetAll();
                return Ok(ApiResponse<List<CategoryDto>>.SuccessResponse(categories));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching categories");
                return StatusCode(500, ApiResponse<string>.FailResponse("Internal server error"));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                _logger.LogInformation($"Fetching category {id}");
                var category = await _categoryService.GetById(id);
                if (category == null)
                    return NotFound(ApiResponse<CategoryDto>.FailResponse("Category not found"));

                return Ok(ApiResponse<CategoryDto>.SuccessResponse(category));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching category {id}");
                return StatusCode(500, ApiResponse<string>.FailResponse("Internal server error"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto dto)
        {
            try
            {
                _logger.LogInformation("Creating new category");
                var result = await _categoryService.Add(dto);
                if (!result)
                    return BadRequest(ApiResponse<string>.FailResponse("Failed to create category"));

                return Ok(ApiResponse<string>.SuccessResponse("Category created successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category");
                return StatusCode(500, ApiResponse<string>.FailResponse("Internal server error"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CategoryDto dto)
        {
            try
            {
                dto.Id = id;
                _logger.LogInformation($"Updating category {id}");
                var result = await _categoryService.Update(dto);
                if (!result)
                    return BadRequest(ApiResponse<string>.FailResponse("Failed to update category"));

                return Ok(ApiResponse<string>.SuccessResponse("Category updated successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating category {id}");
                return StatusCode(500, ApiResponse<string>.FailResponse("Internal server error"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                _logger.LogInformation($"Deleting category {id}");
                var result = await _categoryService.Delete(id);
                if (!result)
                    return BadRequest(ApiResponse<string>.FailResponse("Failed to delete category"));

                return Ok(ApiResponse<string>.SuccessResponse("Category deleted successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting category {id}");
                return StatusCode(500, ApiResponse<string>.FailResponse("Internal server error"));
            }
        } 
        #endregion

    }
}

