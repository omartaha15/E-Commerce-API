using AutoMapper;
using E_Commerce_API.DTOs.CartDTOs;
using E_Commerce_API.DTOs.CategoryDTOs;
using E_Commerce_API.DTOs.OrderDTOs;
using E_Commerce_API.DTOs.ProductDTOs;
using E_Commerce_API.DTOs.ReviewDTOs;
using E_Commerce_API.DTOs.UserDTOs;
using E_Commerce_API.DTOs.UserProfileDTOs;
using E_Commerce_API.Model;

namespace E_Commerce_API
{
    public class MappingProfile : Profile
    {
        public MappingProfile ()
        {
            // User Mappings
            CreateMap<UserRegisterDto, User>().ReverseMap();
            CreateMap<User, UserProfileDto>().ReverseMap();
            CreateMap<UpdateProfileDto, User>().ReverseMap();
            CreateMap<UpdateUserProfileDto, User>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();

            // Product Mappings
            CreateMap<ProductCreateDto, Product>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();
            CreateMap<Product, ProductDetailsDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();

            // Category Mappings
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CreateCategoryDTO, Category>().ReverseMap();
            CreateMap<UpdateCategoryDTO, Category>().ReverseMap();
            CreateMap<CreateCategoryDTO, CategoryDTO>().ReverseMap();



            // Order Mappings
            CreateMap<Order, OrderDetailsDto>()
                .ForMember( dest => dest.OrderItems, opt => opt.MapFrom( src => src.OrderItems ) );
            CreateMap<OrderItem, OrderItemDetailsDto>()
                .ForMember( dest => dest.ProductName, opt => opt.MapFrom( src => src.Product.Name ) );
            CreateMap<OrderCreateDto, Order>();
            CreateMap<OrderItemCreateDto, OrderItem>();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Order, AdminOrderListDto>();

            // Cart Mappings
            CreateMap<AddCartItemDto, CartItem>().ReverseMap();

            // Review Mappings
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<CreateReviewDto, Review>();
        }
    }
}
