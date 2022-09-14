using AutoMapper;
using EPharma.Infrastructure.Models.Identity;
using EPharma.Application.Responses.Identity;

namespace EPharma.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, HrRole>().ReverseMap();
        }
    }
}