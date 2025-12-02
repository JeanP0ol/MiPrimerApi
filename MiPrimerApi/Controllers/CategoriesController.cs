using Api.W.Movies.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MiPrimerApi.DAL.Models.Dtos;

namespace Api.W.Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet(Name = "GetCategoriesAsync")]
        public async Task<ActionResult<ICollection<CategoryDto>>> GetCategoriesAsync()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategoryAsync")]
        public async Task<ActionResult<CategoryDto>> GetCategoryAsync(int id)
        {
            try
            {
                var categoryDto = await _categoryService.GetCategoryAsync(id);
                return Ok(categoryDto);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("No se encontró"))
            {
                return NotFound(new { ex.Message });
            }
        }

        [HttpPost(Name = "CreateCategoryAsync")]
        public async Task<ActionResult<CategoryDto>> CreateCategoryAsync([FromBody] CategoryCreateUpdateDto categoryCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdCategory = await _categoryService.CreateCategoryAsync(categoryCreateDto);

                return CreatedAtRoute("GetCategoryAsync", new { id = createdCategory.Id }, createdCategory);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Ya existe"))
            {
                return Conflict(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}