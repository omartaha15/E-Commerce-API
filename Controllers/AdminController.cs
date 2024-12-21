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
