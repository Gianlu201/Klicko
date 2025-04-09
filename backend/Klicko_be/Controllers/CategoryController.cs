using Klicko_be.DTOs.Category;
using Klicko_be.Models;
using Klicko_be.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Klicko_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();

                if (categories == null)
                {
                    return NotFound();
                }

                var categoriesDto = categories
                    ?.Select(c => new CategorySimpleDto()
                    {
                        CategoryId = c.CategoryId,
                        Name = c.Name,
                        Description = c.Description,
                        Image = c.Image,
                        Icon = c.Icon,
                    })
                    .ToList();

                return Ok(
                    new GetCategoriesListResponseDto()
                    {
                        Message = $"{categoriesDto.Count} categories found!",
                        Categories = categoriesDto,
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequestDto createCategory)
        {
            try
            {
                var newCategory = new Category()
                {
                    CategoryId = Guid.NewGuid(),
                    Name = createCategory.Name,
                    Description = createCategory.Description,
                    Image = createCategory.Image,
                    Icon = createCategory.Icon,
                };

                var result = await _categoryService.CreateCategoryAsync(newCategory);

                return result
                    ? Ok(
                        new CreateCategoryResponseDto()
                        {
                            Message = "Category created successfully!",
                        }
                    )
                    : BadRequest(
                        new CreateCategoryResponseDto() { Message = "Something went wrong!" }
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
