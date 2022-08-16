using AutoMapper;
using DynaCore.Application.Features.ContactPersons.Commands.AddEdit;
using DynaCore.Domain.Entities.Catalog;

namespace DynaCore.Application.Mappings
{
    public class ContactPersonProfile : Profile
    {
        public ContactPersonProfile()
        {
            CreateMap<AddEditContactPersonCommand, ContactPerson>().ReverseMap();
        }
    }
}