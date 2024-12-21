using AutoMapper;
using E_Commerce_API.Data;
using E_Commerce_API.DTOs.OrderDTOs;
using E_Commerce_API.Interfaces;
using E_Commerce_API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<User> userManager;

        public OrderService ( ApplicationDbContext dbContext, IMapper mapper, UserManager<User> userManager )
        {
            _dbContext = dbContext;
            _mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<int> CreateOrderAsync ( OrderCreateDto orderDto )
        {


            var userExists = await userManager.FindByIdAsync( orderDto.UserId );

            if ( userExists == null )
            {
                throw new ArgumentException( "Invalid UserId" );
            }

            foreach ( var item in orderDto.OrderItems )
            {
                var product = await _dbContext.Products.FindAsync( item.ProductId );

                if ( product == null )
                {
                    throw new ArgumentException( $"Product with ID {item.ProductId} does not exist." );
                }

                if ( product.StockQuantity < item.Quantity )
                {
                    throw new InvalidOperationException( $"Insufficient stock for product {product.Name}. Available: {product.StockQuantity}, Requested: {item.Quantity}" );
                }
            }

            var order = _mapper.Map<Order>( orderDto );

            order.TotalAmount = orderDto.OrderItems.Sum( item => item.Quantity * item.UnitPrice );

            foreach ( var item in orderDto.OrderItems )
            {
                var product = await _dbContext.Products.FindAsync( item.ProductId );
                product.StockQuantity -= item.Quantity;
                _dbContext.Products.Update( product );
            }

            await _dbContext.Orders.AddAsync( order );
            await _dbContext.SaveChangesAsync();

            return order.Id;
        }

        public async Task<List<AdminOrderListDto>> GetAllOrdersAsync ()
        {
            var orders = await _dbContext.Orders.ToListAsync();

            return _mapper.Map<List<AdminOrderListDto>>( orders );
        }


        public async Task UpdateOrderAsync ( int orderId, OrderUpdateDto orderUpdateDto )
        {
            var existingOrder = await _dbContext.Orders
                .Include( o => o.OrderItems )
                .ThenInclude( oi => oi.Product )
                .FirstOrDefaultAsync( o => o.Id == orderId );

            if ( existingOrder == null )
            {
                throw new KeyNotFoundException( "Order not found." );
            }

            if ( existingOrder.OrderStatus != "Pending" )
            {
                throw new InvalidOperationException( "Only pending orders can be updated." );
            }

            foreach ( var updatedItem in orderUpdateDto.OrderItems )
            {
                var product = await _dbContext.Products.FindAsync( updatedItem.ProductId );
                if ( product == null )
                {
                    throw new ArgumentException( $"Product with ID {updatedItem.ProductId} does not exist." );
                }

                var existingItem = existingOrder.OrderItems.FirstOrDefault( oi => oi.ProductId == updatedItem.ProductId );
                int currentStockAdjustment = existingItem != null ? existingItem.Quantity : 0;

                if ( product.StockQuantity + currentStockAdjustment < updatedItem.Quantity )
                {
                    throw new InvalidOperationException( $"Insufficient stock for product {product.Name}. Available: {product.StockQuantity + currentStockAdjustment}, Requested: {updatedItem.Quantity}" );
                }
            }

            foreach ( var existingItem in existingOrder.OrderItems )
            {
                var product = existingItem.Product;
                product.StockQuantity += existingItem.Quantity; 
                _dbContext.Products.Update( product );
            }

            foreach ( var updatedItem in orderUpdateDto.OrderItems )
            {
                var product = await _dbContext.Products.FindAsync( updatedItem.ProductId );
                if ( product != null )
                {
                    product.StockQuantity -= updatedItem.Quantity; 
                    _dbContext.Products.Update( product );
                }
            }

            
            existingOrder.OrderItems.Clear(); 
            existingOrder.OrderItems = _mapper.Map<List<OrderItem>>( orderUpdateDto.OrderItems ); 
            existingOrder.TotalAmount = orderUpdateDto.OrderItems.Sum( item => item.Quantity * item.TotalAmount );
            existingOrder.ShippingAddress = orderUpdateDto.ShippingAddress;
            existingOrder.OrderStatus = orderUpdateDto.OrderStatus;

            await _dbContext.SaveChangesAsync();
        }



        public async Task<OrderDetailsDto> GetOrderByIdAsync ( int id )
        {
            var order = await _dbContext.Orders
               .Include( o => o.OrderItems )
               .ThenInclude( oi => oi.Product )
               .FirstOrDefaultAsync( o => o.Id == id );

            return _mapper.Map<OrderDetailsDto>( order );
        }

        public async Task<List<OrderDetailsDto>> GetUserOrdersAsync ( string userId )
        {
            var orders = await _dbContext.Orders
                .Include( o => o.OrderItems )
                .ThenInclude( oi => oi.Product )
                .Where( o => o.UserId == userId )
                .ToListAsync();

            return _mapper.Map<List<OrderDetailsDto>>( orders );
        }

        public async Task UpdateOrderStatusAsync ( int id, string status )
        {
            var order = await _dbContext.Orders.FindAsync( id );
            if ( order == null )
            {
                throw new KeyNotFoundException( "Order not found." );
            }

            order.OrderStatus = status;
            await _dbContext.SaveChangesAsync();
        }

        public async Task CancelOrderAsync ( int id )
        {
            var order = await _dbContext.Orders
                .Include( o => o.OrderItems )
                .ThenInclude( oi => oi.Product )
                .FirstOrDefaultAsync( o => o.Id == id );

            if ( order == null )
            {
                throw new KeyNotFoundException( "Order not found." );
            }

            if ( order.OrderStatus != "Pending" )
            {
                throw new InvalidOperationException( "Only pending orders can be canceled." );
            }

            foreach ( var item in order.OrderItems )
            {
                var product = item.Product;
                product.StockQuantity += item.Quantity;
                _dbContext.Products.Update( product );
            }

            order.OrderStatus = "Canceled";
            await _dbContext.SaveChangesAsync();
        }
    }
}
