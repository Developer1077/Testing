using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

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

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();


        }

    }
}
