using AutoMapper;
using Client.Models;

namespace Client.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, BasketItem>()
               .ForMember(
                    dest => dest.ProductName,
                    options => options.MapFrom(src => src.Name)
                )
               .ForMember(
                    dest => dest.Brand,
                    options => options.MapFrom(src => src.ProductBrand)
                )
                .ForMember(
                    dest => dest.Type,
                    options => options.MapFrom(src => src.ProductType)
                );

        }

    }
}
