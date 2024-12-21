using E_Commerce_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    [Authorize( Roles = "Admin" )]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController ( IAdminService adminService )
        {
            _adminService = adminService;
        }

        [HttpGet( "dashboard/sales-summary" )]
        [ResponseCache(Duration = 5)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSalesSummary ()
        {
            try
            {
                var summary = await _adminService.GetSalesSummaryAsync();
                return Ok( summary );
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpGet( "users" )]
        [ResponseCache( Duration = 30 )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        public async Task<IActionResult> GetAllUsers ()
        {
            try
            {
                var users = await _adminService.GetAllUsersAsync();
                return Ok( users );
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpGet( "products/low-stock" )]
        [ResponseCache( Duration = 5 )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        public async Task<IActionResult> GetLowStockProducts ()
        {
            try
            {
                var products = await _adminService.GetLowStockProductsAsync();
                return Ok( products );
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpGet( "orders/recent" )]
        [ResponseCache( Duration = 5 )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        public async Task<IActionResult> GetRecentOrders ()
        {
            try
            {
                var orders = await _adminService.GetRecentOrdersAsync();
                return Ok( orders );
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }
    }
}
