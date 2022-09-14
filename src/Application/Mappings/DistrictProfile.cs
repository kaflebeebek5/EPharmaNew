using AutoMapper;
using EPharma.Application.Features.Province.Queries.GetById;
using EPharma.Domain.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Mappings
{
    public class DistrictProfile : Profile
    {
        public DistrictProfile()
        {
            CreateMap<GetDistrictByIdQuery, District>().ReverseMap();
        }

    }
}
