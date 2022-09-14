using AutoMapper;
using EPharma.Application.Requests.Identity;
using EPharma.Application.Responses.Identity;
using EPharma.Infrastructure.Models.Identity;

namespace EPharma.Infrastructure.Mappings
{
    public class RoleClaimProfile : Profile
    {
        public RoleClaimProfile()
        {
            CreateMap<RoleClaimResponse, HrRoleClaim>()
                .ForMember(nameof(HrRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(HrRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();

            CreateMap<RoleClaimRequest, HrRoleClaim>()
                .ForMember(nameof(HrRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(HrRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();
        }
    }
}