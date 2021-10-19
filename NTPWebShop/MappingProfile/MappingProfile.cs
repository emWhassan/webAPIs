using AutoMapper;
using NTPWebShop.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTPWebShop.API.MappingProfile
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto, Domain.Product>();
            CreateMap<Domain.Product, ProductDto>();
            CreateMap<OrderDto, Domain.Order>();
        }
    }
}
