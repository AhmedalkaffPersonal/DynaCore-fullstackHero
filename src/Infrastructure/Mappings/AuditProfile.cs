using AutoMapper;
using DynaCore.Infrastructure.Models.Audit;
using DynaCore.Application.Responses.Audit;

namespace DynaCore.Infrastructure.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditResponse, Audit>().ReverseMap();
        }
    }
}