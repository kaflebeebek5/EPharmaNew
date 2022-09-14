using AutoMapper;
using EPharma.Application.Features.StaticVariable.Queries.GetAll;
using EPharma.Application.Features.StaticVariable.Queries.GetByName;
using EPharma.Domain.Entities.Settings;

namespace EPharma.Application.Mappings
{
    public class StaticVariableProfile : Profile
    {
        public StaticVariableProfile()
        {
            CreateMap<GetAllStaticVariableResponse, StaticVariable>().ReverseMap();
            CreateMap<GetStaticVariableByNameResponse, StaticVariable>().ReverseMap();
        }
    }
}
