using AutoMapper;
using DynaCore.Application.Requests.Identity;
using DynaCore.Application.Responses.Identity;

namespace DynaCore.Client.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<PermissionResponse, PermissionRequest>().ReverseMap();
            CreateMap<RoleClaimResponse, RoleClaimRequest>().ReverseMap();
        }
    }
}