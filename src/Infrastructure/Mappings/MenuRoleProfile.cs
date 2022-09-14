using AutoMapper;
using EPharma.Application.Requests.Identity;
using EPharma.Application.Responses.Identity;
using EPharma.Infrastructure.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Infrastructure.Mappings
{
   public class MenuRoleProfile: Profile
    {
        public MenuRoleProfile()
        {
            CreateMap<MenuRoleResponse,MenuRole>().ReverseMap();
            CreateMap<MenuRoleRequest, MenuRole>().ReverseMap();
        }
    }
}
