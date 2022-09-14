using AutoMapper;
using EPharma.Application.Requests.Identity;
using EPharma.Application.Responses.Identity;

namespace EPharma.Client.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<PermissionResponse, PermissionRequest>().ReverseMap();
            CreateMap<RoleClaimResponse, RoleClaimRequest>().ReverseMap();
        }
    }
}