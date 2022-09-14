using AutoMapper;
using EPharma.Application.Features.MasterTableSetup.Commands.AddEdit;
using EPharma.Application.Features.MasterTableSetup.Queries.GetAll;
using EPharma.Application.Features.MasterTableSetup.Queries.GetById;
using EPharma.Domain.Entities.Settings;

namespace EPharma.Application.Mappings
{
    class MasterTableSetupProfile : Profile
    {
        public MasterTableSetupProfile()
        {
            CreateMap<AddEditMasterTableSetupCommand, MasterTableSetup>().ReverseMap();
            CreateMap<GetAllMasterTableSetupResponse, MasterTableSetup>().ReverseMap();
            CreateMap<GetMasterTableSetupByIdResponse, MasterTableSetup>().ReverseMap();
        }
    }
}
