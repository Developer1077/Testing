using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
               .ForMember(
                    dest => dest.ProductBrand,
                    options => options.MapFrom(src => src.ProductBrand.Name)
                )
                .ForMember(
                    dest => dest.ProductType,
                    options => options.MapFrom(src => src.ProductType.Name)
                )
                .ForMember(
                    dest => dest.PictureUrl,
                    options => options.MapFrom<ProductUrlResolver>()
                );

            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dest=> dest.DeliveryMethod, options=>options.MapFrom(src=>src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.ShippingPrice, options => options.MapFrom(src => src.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, options => options.MapFrom(src => src.ItemOrdered.ProductItemId))
                .ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.ItemOrdered.ProductName))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom(src => src.ItemOrdered.PictureUrl))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<OrderItemUrlResolver>());



        }

    }
}
