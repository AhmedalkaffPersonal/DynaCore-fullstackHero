using AutoMapper;
using DynaCore.Application.Features.Products.Commands.AddEdit;
using DynaCore.Domain.Entities.Catalog;

namespace DynaCore.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddEditProductCommand, Product>().ReverseMap();
        }
    }
}