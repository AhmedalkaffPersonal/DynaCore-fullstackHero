using AutoMapper;
using DynaCore.Infrastructure.Models.Identity;
using DynaCore.Application.Responses.Identity;

namespace DynaCore.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, BlazorHeroRole>().ReverseMap();
        }
    }
}