using AutoMapper;
using EPharma.Application.Features.Vdc.Queries.GetById;
using EPharma.Domain.Entities.Settings;

namespace EPharma.Application.Mappings
{
    public class LocalBodiesProfile : Profile
    {
        public LocalBodiesProfile()
        {
            CreateMap<GetVdcByIdQuery, LocalBodies>().ReverseMap();
        }
    }
}
