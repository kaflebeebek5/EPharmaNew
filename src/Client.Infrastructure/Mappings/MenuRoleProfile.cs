using AutoMapper;
using EPharma.Application.Requests.Identity;
using EPharma.Application.Responses.Identity;

namespace EPharma.Client.Infrastructure.Mappings
{
    public class MenuRoleProfile : Profile
    {
        public MenuRoleProfile()
        {
            CreateMap<MenuRoleResponse, MenuRoleRequest>().ReverseMap();
        }
    }
}
