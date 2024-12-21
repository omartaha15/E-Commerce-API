﻿using E_Commerce_API.DTOs.OrderDTOs;
using E_Commerce_API.Interfaces;
using E_Commerce_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce_API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    [Authorize]

    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController ( IOrderService orders )
        {
            _orderService = orders;
        }

        [HttpGet( "admin" )]
        [Authorize( Roles = "Admin" )]
        public async Task<IActionResult> GetAllOrders ()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok( orders );
        }

        [HttpGet]
        public async Task<IActionResult> GetUserOrders ()
        {
            var userId = User.FindFirstValue( ClaimTypes.NameIdentifier );
            if ( string.IsNullOrEmpty( userId ) )
                return Unauthorized( new { message = "User not authenticated" } );


            var orders = await _orderService.GetUserOrdersAsync( userId );
            return Ok( orders );
        }

        [HttpGet( "{id}" )]
        public async Task<IActionResult> GetOrderById ( int id )
        {
            var order = await _orderService.GetOrderByIdAsync( id );
            if ( order == null ) return NotFound();
            return Ok( order );
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder ( [FromBody] OrderCreateDto orderDto )
        {
            try
            {

                var orderId = await _orderService.CreateOrderAsync( orderDto );
                return CreatedAtAction( nameof( GetOrderById ), new { id = orderId }, null );
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(int id , [FromBody] OrderUpdateDto orderDto )
        {
            try
            {
                await _orderService.UpdateOrderAsync( id, orderDto );
                return Ok();
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> CancleOrder(int id )
        {
            try
            {
                await _orderService.CancelOrderAsync( id );
                return Ok();
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, new { message = ex.Message } );
            }
        }

        [HttpPut( "{id}/status" )]
        [Authorize( Roles = "Admin" )]
        public async Task<IActionResult> UpdateOrderStatus ( int id, [FromBody] OrderStatusUpdateDto statusDto )
        {
            await _orderService.UpdateOrderStatusAsync( id, statusDto.OrderStatus );
            return NoContent();
        }
    }
}