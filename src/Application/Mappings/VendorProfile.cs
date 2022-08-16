using AutoMapper;
using DynaCore.Application.Features.Vendors.Commands.AddEdit;
using DynaCore.Application.Features.Vendors.Queries.GetAll;
using DynaCore.Application.Features.Vendors.Queries.GetById;
using DynaCore.Domain.Entities.Catalog;

namespace DynaCore.Application.Mappings
{
    public class VendorProfile : Profile
    {
        public VendorProfile()
        {
            CreateMap<AddEditVendorCommand, Vendor>().ReverseMap();
            CreateMap<GetVendorByIdResponse, Vendor>().ReverseMap();
            CreateMap<GetAllVendorsResponse, Vendor>().ReverseMap();
        }
    }
}