using AutoMapper;
using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using DataAccessLayer.Models;


namespace BusinessLogicLayer.DTO
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile() 
        {
            CreateMap<CategoryRequestDto, Category>();
            CreateMap<OrderItemRequestDto, OrderItem>();
            CreateMap<OrderRequestDto, Order>();
            CreateMap<ProductRequestDto, Product>();
            CreateMap<UserRequestDto, User>();

            CreateMap<Category, CategoryResponseDto>();
            CreateMap<OrderItem, OrderItemResponseDto>();
            CreateMap<Order, OrderResponseDto>();
            CreateMap<Product, ProductResponseDto>();
            CreateMap<User, UserResponseDto>();
        }
    }
}
