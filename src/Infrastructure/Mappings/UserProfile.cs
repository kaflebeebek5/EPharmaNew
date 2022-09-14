using AutoMapper;
using EPharma.Infrastructure.Models.Identity;
using EPharma.Application.Responses.Identity;

namespace EPharma.Infrastructure.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserResponse, HrUser>().ReverseMap();
            CreateMap<ChatUserResponse, HrUser>().ReverseMap()
                .ForMember(dest => dest.EmailAddress, source => source.MapFrom(source => source.Email)); //Specific Mapping
        }
    }
}