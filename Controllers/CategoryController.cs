using E_Commerce_API.DTOs.CategoryDTOs;
using E_Commerce_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet("GetAll")]
        [ResponseCache( Duration = 30 )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var categories = await categoryService.GetAllAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpGet("{id}")]
        [ResponseCache( Duration = 30 )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        public async Task<ActionResult> GetCategory(int id )
        {
            try
            {
                var category = await categoryService.GetCategoryByIdAsync( id );
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpPost]
        [Authorize( Roles = "Admin" )]
        public async Task<ActionResult> CreateCategory(CreateCategoryDTO categoryDTO)
        {
            try
            {
                var category = await categoryService.CreateCategoryAsync( categoryDTO );
                return CreatedAtAction( nameof( GetCategory ), new { id = category.Id }, category );
            }
            catch (Exception ex)
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpPut("{id}")]
        [Authorize( Roles = "Admin" )]
        public async Task<ActionResult> UpdateCategory(int id , UpdateCategoryDTO categoryDTO )
        {
            try
            {
                var category = await categoryService.UpdateCategoryAsync( id , categoryDTO );
                return Ok(category);
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpDelete("{id}")]
        [Authorize( Roles = "Admin" )]
        public async Task<ActionResult> DeleteCategory(int id )
        {
            try
            {
                await categoryService.DeleteCategoryAsync( id );
                return Ok( "Deleted Successfully" );
            }
            catch (Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }
    }
}
