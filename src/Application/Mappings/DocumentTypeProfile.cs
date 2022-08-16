using AutoMapper;
using DynaCore.Application.Features.DocumentTypes.Commands.AddEdit;
using DynaCore.Application.Features.DocumentTypes.Queries.GetAll;
using DynaCore.Application.Features.DocumentTypes.Queries.GetById;
using DynaCore.Domain.Entities.Misc;

namespace DynaCore.Application.Mappings
{
    public class DocumentTypeProfile : Profile
    {
        public DocumentTypeProfile()
        {
            CreateMap<AddEditDocumentTypeCommand, DocumentType>().ReverseMap();
            CreateMap<GetDocumentTypeByIdResponse, DocumentType>().ReverseMap();
            CreateMap<GetAllDocumentTypesResponse, DocumentType>().ReverseMap();
        }
    }
}