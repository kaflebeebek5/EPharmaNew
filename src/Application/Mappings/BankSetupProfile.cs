using AutoMapper;
using EPharma.Application.Features.BankSetup.Commands.AddEdit;
using EPharma.Application.Features.BankSetup.Queries.GetAll;
using EPharma.Application.Features.BankSetup.Queries.GetById;
using EPharma.Domain.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Mappings
{
    public class BankSetupProfile : Profile
    {
        public BankSetupProfile()
        {
            CreateMap<AddEditBankSetupCommand, BankSetup>().ReverseMap();
            CreateMap<GetBankSetupByIdResponse, BankSetup>().ReverseMap();
            CreateMap<GetAllBankSetupResponse, BankSetup>().ReverseMap();
        }
    }
}
