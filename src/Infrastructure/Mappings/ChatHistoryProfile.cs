using AutoMapper;
using DynaCore.Application.Interfaces.Chat;
using DynaCore.Application.Models.Chat;
using DynaCore.Infrastructure.Models.Identity;

namespace DynaCore.Infrastructure.Mappings
{
    public class ChatHistoryProfile : Profile
    {
        public ChatHistoryProfile()
        {
            CreateMap<ChatHistory<IChatUser>, ChatHistory<BlazorHeroUser>>().ReverseMap();
        }
    }
}