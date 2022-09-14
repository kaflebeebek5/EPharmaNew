using AutoMapper;
using EPharma.Application.Interfaces.Chat;
using EPharma.Application.Models.Chat;
using EPharma.Infrastructure.Models.Identity;

namespace EPharma.Infrastructure.Mappings
{
    public class ChatHistoryProfile : Profile
    {
        public ChatHistoryProfile()
        {
            CreateMap<ChatHistory<IChatUser>, ChatHistory<HrUser>>().ReverseMap();
        }
    }
}