using AutoMapper;
using DynaCore.Application.Features.Documents.Commands.AddEdit;
using DynaCore.Application.Features.Documents.Queries.GetById;
using DynaCore.Domain.Entities.Misc;

namespace DynaCore.Application.Mappings
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<AddEditDocumentCommand, Document>().ReverseMap();
            CreateMap<GetDocumentByIdResponse, Document>().ReverseMap();
        }
    }
}