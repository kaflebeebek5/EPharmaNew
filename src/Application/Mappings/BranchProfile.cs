using AutoMapper;
using EPharma.Application.Features.Branch.Commands.AddEdit;
using EPharma.Application.Features.Branch.Queries.GetAll;
using EPharma.Application.Features.Branch.Queries.GetById;
using EPharma.Domain.Entities.Settings;

namespace EPharma.Application.Mappings
{
    public class BranchProfile : Profile
    {
        public BranchProfile()
        {
            CreateMap<AddEditBranchCommand, Branch>().ReverseMap();
            CreateMap<GetAllBranchResponse, Branch>().ReverseMap();
            CreateMap<GetBranchByIdResponse, Branch>().ReverseMap();
        }
    }
}
