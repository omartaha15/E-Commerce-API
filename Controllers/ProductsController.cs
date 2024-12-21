using AutoMapper;
using E_Commerce_API.DTOs.ProductDTOs;
using E_Commerce_API.Interfaces;
using E_Commerce_API.Model;
using E_Commerce_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _products;
        private readonly IMapper _mapper;

        public ProductsController(IProductService product, IMapper mapper)
        {
            _products = product;
            _mapper = mapper;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult> GetAllProducts()
        {
            try
            {
                var products = await _products.GetAllAsync();
                return Ok(products);
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts ( [FromQuery] ProductSearchDto searchDto )
        {
            try
            {
                var (products, totalCount) = await _products.GetProductsAsync( searchDto );
                return Ok( new { totalCount, products } );
            }
            catch (Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }        

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id )
        {
            try
            {
                var product = await _products.GetProductByIdAsync( id );
                return Ok( product );
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpPost("Create")]
        [Authorize( Roles = "Admin" )]
        public async Task<ActionResult> CreateProduct ( [FromBody] ProductCreateDto productDto)
        {
            try
            {
                var product = await _products.CreateProductAsync( productDto );

                return CreatedAtAction( nameof( GetProduct ), new { id = product.Id }, product );

            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpPut("{id}")]
        [Authorize( Roles = "Admin" )]
        public async Task<ActionResult> UpdateProduct ( int id, [FromBody] ProductUpdateDto productDto )
        {
            try
            {
                await _products.UpdateProductAsync( id, productDto );
                return Ok();
            }
            catch (Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpDelete( "{id}" )]
        [Authorize( Roles = "Admin" )]
        public async Task<IActionResult> DeleteProduct ( int id )
        {
            try
            {
                await _products.DeleteProductAsync( id );
                return NoContent();
            }
            catch( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpGet( "search" )]
        public async Task<IActionResult> SearchProducts ( [FromQuery] string query )
        {
            try
            {
                var products = await _products.SearchProductsAsync( query );
                return Ok( products );
            }
            catch( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }

        }
    }
}
