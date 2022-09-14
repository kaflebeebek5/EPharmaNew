using AutoMapper;
using EPharma.Application.Requests;
using EPharma.Domain.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Mappings
{
    public class MedicineProfile : Profile
    {
        public MedicineProfile()
        {
            CreateMap<MedicineRequestModel,UserMedicine>().ReverseMap();
        }
    }
}
