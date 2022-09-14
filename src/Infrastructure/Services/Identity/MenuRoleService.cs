using AutoMapper;
using EPharma.Application.Interfaces.Services;
using EPharma.Application.Interfaces.Services.Identity;
using EPharma.Application.Requests.Identity;
using EPharma.Application.Responses.Identity;
using EPharma.Infrastructure.Contexts;
using EPharma.Infrastructure.Models.Identity;
using EPharma.Shared.Wrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Infrastructure.Services.Identity
{
    public class MenuRoleService:IMenuRoleService
    {
        private readonly IStringLocalizer<MenuRoleService> _localizer;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly EPharmaContext _db;
        private readonly UserManager<HrUser> _userManager;

        public MenuRoleService(
        IStringLocalizer<MenuRoleService> localizer,
        IMapper mapper,
        ICurrentUserService currentUserService,
        EPharmaContext db,
        UserManager<HrUser> userManager)
        {
            _localizer = localizer;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _db = db;
            _userManager= userManager;
        }
        public async Task<Result<string>> DeleteAsync(int id)
        {
            var menurole = await _db.MenuRole.SingleOrDefaultAsync(x => x.Id == id);
            if (menurole != null)
            {
                _db.MenuRole.Remove(menurole);
                await _db.SaveChangesAsync();
                return await Result<string>.SuccessAsync(_localizer["Role not Deleted"]);
            }
            else
            {
                return await Result<string>.FailAsync(_localizer["Role Not Select"]);
            }

        }
        public async Task<Result<List<MenuRoleResponse>>> GetAllAsync()
        {
            var menurole = await _db.MenuList.GroupJoin(_db.MenuRole, ml => ml.Id, mr => mr.MenuId, (ml, mr) => new
            {
                ml,
                mr
            }).SelectMany(x => x.mr.DefaultIfEmpty(), (menulist, menurole) => new
            {
                Id = menurole == null ? 0 : menurole.Id,
                MenuId = menurole == null ? 0 : menurole.MenuId,
                MenuListId = menulist.ml.Id,
                MenuName = menulist.ml.MenuName,
                ParentName = menulist.ml.ParentItem.MenuName,
                RoleId = menurole.RoleId,
                IsChecked = menurole.RoleId == null ? false : true
            }).Select(e => new MenuRoleResponse
            {
                Id = e == null ? 0 : e.Id,
                MenuId = e == null ? 0 : e.MenuId,
                MenuListId = e.MenuListId,
                MenuName = e.MenuName,
                ParentItem = e.ParentName,
                IsChecked = e.RoleId == null ? false : true
            }).ToListAsync();

            var menuroleResponse = _mapper.Map<List<MenuRoleResponse>>(menurole);
            return await Result<List<MenuRoleResponse>>.SuccessAsync(menuroleResponse);
        }

        public async Task<Result<List<MenuRoleResponse>>> GetByIdAsync(string Id)
        {
            var menurole = await _db.MenuList.GroupJoin(_db.MenuRole.Where(x=>x.RoleId==Id), ml => ml.Id, mr => mr.MenuId, (ml, mr) => new
            {
                ml,
                mr
            }).SelectMany(x => x.mr.DefaultIfEmpty(), (menulist, menurole) => new
            {
                Id = menurole == null ? 0 : menurole.Id,
                MenuId = menurole == null ? 0 : menurole.MenuId,
                MenuListId = menulist.ml.Id,
                MenuName = menulist.ml.MenuName,
                ParentName = menulist.ml.ParentItem.MenuName,
                RoleId = menurole.RoleId,
                IsChecked = menurole.RoleId == null ? false : true
            }).Select(e => new MenuRoleResponse
            {
                Id = e == null ? 0 : e.Id,
                MenuId = e == null ? 0 : e.MenuId,
                MenuListId = e.MenuListId,
                MenuName = e.MenuName,
                ParentItem = e.ParentName,
                IsChecked = e.RoleId == null ? false:true 
            }).ToListAsync();

            var menuroleResponse = _mapper.Map<List<MenuRoleResponse>>(menurole);
            return await Result<List<MenuRoleResponse>>.SuccessAsync(menuroleResponse);
        }

        public async Task<Result<List<MenuRoleResponse>>> GetUserMenuListAsync()
        {
            var currentUser = await _userManager.Users.SingleAsync(x => x.Id == _currentUserService.UserId);
            var roleList = await _userManager.GetRolesAsync(currentUser);

            var menurole = await _db.MenuList.Join(_db.MenuRole.Where(x =>  roleList.Contains(x.Role.Name)), ml => ml.Id, mr => mr.MenuId, (ml, mr) => new MenuRoleResponse
            {
                MenuName = ml.MenuName,
                MenuNameNepali = ml.MenuNameNepali,
                ParentItem = ml.ParentItem.MenuName,
                Path = ml.Path,
                Icon = ml.Icon
            }).ToListAsync();

            var menuroleResponse = _mapper.Map<List<MenuRoleResponse>>(menurole);
            return await Result<List<MenuRoleResponse>>.SuccessAsync(menuroleResponse);
        }

        public Task<int> GetCountAsync()
        {
            throw new System.NotImplementedException();
        }
        public async Task<Result<string>> SaveAsync(List<MenuRoleRequest> request)
        {
            foreach (var item in request)
            {
                item.MenuId = item.MenuListId;
                if (item.IsChecked == true)
                {
                    if (item.Id == 0)
                    {
                        var menurolelist = _mapper.Map<MenuRole>(item);
                        await _db.MenuRole.AddAsync(menurolelist);
                        await _db.SaveChangesAsync();
                    }
                }
                else
                {
                    var menurole = await _db.MenuRole.SingleOrDefaultAsync(x => x.Id == item.Id);
                    if (menurole != null)
                    {
                        _db.MenuRole.Remove(menurole);
                        await _db.SaveChangesAsync();
                    }
                }
            }
            return await Result<string>.SuccessAsync(_localizer["Role Updated"]);
        }
    }
}
