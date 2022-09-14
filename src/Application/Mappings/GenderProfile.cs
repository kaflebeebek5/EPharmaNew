using AutoMapper;
using EPharma.Application.Features.Genders.Commands.AddEdit;
using EPharma.Application.Features.Genders.Queries.GetAll;
using EPharma.Application.Features.Genders.Queries.GetById;
using EPharma.Domain.Entities.Settings;

namespace EPharma.Application.Mappings
{
    public class GenderProfile : Profile
    {
        public GenderProfile()
        {
            CreateMap<AddEditGenderCommand, Gender>().ReverseMap();
            CreateMap<GetAllGendersResponse, Gender>().ReverseMap();
            CreateMap<GetGenderByIdResponse, Gender>().ReverseMap();
        }
    }
}
