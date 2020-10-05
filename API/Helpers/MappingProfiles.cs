﻿using API.Dtos;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class MappingProfiles :Profile
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

        }
        
    }
}
