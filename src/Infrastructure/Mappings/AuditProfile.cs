using AutoMapper;
using EPharma.Infrastructure.Models.Audit;
using EPharma.Application.Responses.Audit;

namespace EPharma.Infrastructure.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditResponse, Audit>().ReverseMap();
        }
    }
}