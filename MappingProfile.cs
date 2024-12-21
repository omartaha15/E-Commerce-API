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
            CreateMap<UserRegisterDto, User>().ReverseMap();
            CreateMap<User, UserProfileDto>().ReverseMap();
            CreateMap<UpdateProfileDto, User>().ReverseMap();
            CreateMap<UpdateUserProfileDto, User>().ReverseMap();
            CreateMap<UserProfileDto, UpdateUserProfileDto>().ReverseMap();

            CreateMap<ProductCreateDto, Product>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();
            CreateMap<Product, ProductDetailsDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();


            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CreateCategoryDTO, Category>().ReverseMap();
            CreateMap<CreateCategoryDTO, CategoryDTO>().ReverseMap();
            CreateMap<UpdateCategoryDTO, Category>().ReverseMap();
            CreateMap<UpdateCategoryDTO, CategoryDTO>().ReverseMap();


            CreateMap<Order, OrderDetailsDto>()
           .ForMember( dest => dest.OrderItems, opt => opt.MapFrom( src => src.OrderItems ) );

            CreateMap<OrderItem, OrderItemDetailsDto>()
                .ForMember( dest => dest.ProductName, opt => opt.MapFrom( src => src.Product.Name ) );

            CreateMap<OrderCreateDto, Order>();
            CreateMap<OrderItemCreateDto, OrderItem>();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Order, AdminOrderListDto>();


            CreateMap<AddCartItemDto, CartItem>().ReverseMap();

            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<CreateReviewDto, Review>();

            CreateMap<User, UserDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();


          

        }
    }
}
