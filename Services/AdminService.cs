using AutoMapper;
using E_Commerce_API.Data;
using E_Commerce_API.DTOs;
using E_Commerce_API.DTOs.OrderDTOs;
using E_Commerce_API.DTOs.ProductDTOs;
using E_Commerce_API.DTOs.UserDTOs;
using E_Commerce_API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public AdminService(ApplicationDbContext dbContext, IMapper mapper )
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync ()
        {
            var users = await _dbContext.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>( users );
        }

        public async Task<IEnumerable<ProductDto>> GetLowStockProductsAsync ()
        {
            var lowStockProducts = await _dbContext.Products
               .Where( p => p.StockQuantity < 10 )
               .ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>( lowStockProducts );
        }

        public async Task<IEnumerable<OrderDto>> GetRecentOrdersAsync ()
        {
            var recentOrders = await _dbContext.Orders
                .OrderByDescending( o => o.OrderDate )
                .Take( 10 )
                .Include( o => o.OrderItems )
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderDto>>( recentOrders );
        }

        public async Task<SalesSummaryDto> GetSalesSummaryAsync ()
        {
            var totalSales = await _dbContext.Orders
                .Where( o => o.OrderStatus != "Canceled" )
                .SumAsync( o => o.TotalAmount );

            var totalOrders = await _dbContext.Orders
                .CountAsync( o => o.OrderStatus != "Canceled" );

            var totalProductsSold = await _dbContext.OrderItems
                .Where( oi => oi.Order.OrderStatus != "Canceled" )
                .SumAsync( oi => oi.Quantity );

            return new SalesSummaryDto { TotalSales = totalSales, TotalOrders = totalOrders, TotalProductsSold = totalProductsSold };
        }
    }
}
