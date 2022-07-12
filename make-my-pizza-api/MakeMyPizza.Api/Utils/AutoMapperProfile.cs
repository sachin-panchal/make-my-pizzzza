using AutoMapper;

using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.Dtos;

namespace MakeMyPizza.Api.Utils;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDetailsDto>();
        CreateMap<User, UserRegisterDto>();
        CreateMap<User, UserLoginDto>();
        CreateMap<User, UserDto>();
        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(sourceMember => sourceMember.Orders));

        CreateMap<Order, OrderDto>()
        .ForMember(dest => dest.Pizzas, opt => opt.MapFrom(sourceMember => sourceMember.Pizzas))
        .ForMember(dest => dest.NonPizzas, opt => opt.MapFrom(sourceMember => sourceMember.NonPizzas))
        .ForMember(dest => dest.Drinks, opt => opt.MapFrom(sourceMember => sourceMember.Drinks));
        
        CreateMap<OrderPizza, OrderPizzaDto>()
        .ForMember(dest => dest.Sauces, opt => opt.MapFrom(sourceMember => sourceMember.Sauces))
        .ForMember(dest => dest.Cheese, opt => opt.MapFrom(sourceMember => sourceMember.Cheese))
        .ForMember(dest => dest.Toppings, opt => opt.MapFrom(sourceMember => sourceMember.Toppings));

        CreateMap<OrderNonPizza, OrderNonPizzaDto>();
        CreateMap<OrderDrink, OrderDrinkDto>();
        CreateMap<OrderSauce, OrderSauceDto>();
        CreateMap<OrderTopping, OrderToppingDto>();
        CreateMap<OrderCheese, OrderCheeseDto>();
        CreateMap<Pizza, PizzaDetailDto>();


        CreateMap<UserDetailsDto, User>();
        CreateMap<UserRegisterDto, User>();
        CreateMap<UserLoginDto, User>();
        CreateMap<UserDto, User>();
        CreateMap<CustomerDto, Customer>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(sourceMember => sourceMember.Orders));

        
        CreateMap<OrderNonPizzaDto, OrderNonPizza>();
        CreateMap<OrderDrinkDto, OrderDrink>();
        CreateMap<OrderSauceDto, OrderSauce>();
        CreateMap<OrderToppingDto, OrderTopping>();
        CreateMap<OrderCheeseDto, OrderCheese>();

        CreateMap<OrderPizzaDto, OrderPizza>()
        .ForMember(dest => dest.Sauces, opt => opt.MapFrom(sourceMember => sourceMember.Sauces))
        .ForMember(dest => dest.Cheese, opt => opt.MapFrom(sourceMember => sourceMember.Cheese))
        .ForMember(dest => dest.Toppings, opt => opt.MapFrom(sourceMember => sourceMember.Toppings));

        CreateMap<OrderDto, Order>()
        .ForMember(dest => dest.Pizzas, opt => opt.MapFrom(sourceMember => sourceMember.Pizzas))
        .ForMember(dest => dest.NonPizzas, opt => opt.MapFrom(sourceMember => sourceMember.NonPizzas))
        .ForMember(dest => dest.Drinks, opt => opt.MapFrom(sourceMember => sourceMember.Drinks));

        
    }
}