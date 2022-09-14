using AutoMapper;
using EPharma.Infrastructure.Models.Identity;
using EPharma.Application.Responses.Identity;
using EPharma.Application.Requests.Identity;

namespace EPharma.Infrastructure.Mappings
{
    public class MenuListProfile : Profile
    {
        public MenuListProfile()
        {
            CreateMap<MenuListResponse, MenuList>().ReverseMap();
            CreateMap<MenuListRequest, MenuList>().ReverseMap();
        }
    }
}