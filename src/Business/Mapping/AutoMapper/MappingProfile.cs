using AutoMapper;
using Entities;
using Entities.Dtos;

namespace Business.Mapping.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<CartItemDto, CartItem>();
            CreateMap<CartDto, Cart>();
        }
    }
}