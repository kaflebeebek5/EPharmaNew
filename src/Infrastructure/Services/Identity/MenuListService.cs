using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using EPharma.Application.Interfaces.Services;
using EPharma.Application.Interfaces.Services.Identity;
using EPharma.Application.Requests.Identity;
using EPharma.Application.Responses.Identity;
using EPharma.Infrastructure.Contexts;
using EPharma.Infrastructure.Models.Identity;
using EPharma.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EPharma.Infrastructure.Services.Identity
{
    public class MenuListService : IMenuListService
    {
        private readonly IStringLocalizer<MenuListService> _localizer;
        private readonly IMapper _mapper;
        private MenuListResponse _hrmenu = new();
        // private readonly IHrMenuListService _hrmenuService;
        private readonly ICurrentUserService _currentUserService;
        private readonly EPharmaContext _db;

        public MenuListService(
        IStringLocalizer<MenuListService> localizer,
        IMapper mapper,
        ICurrentUserService currentUserService,
        EPharmaContext db)
        {
            _localizer = localizer;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _db = db;
        }

        public async Task<Result<int>> DeleteAsync(int id)
        {
            var hrmenu = await _db.MenuList.SingleOrDefaultAsync(x => x.Id == id);
            if(hrmenu != null)
            {
                var hrrole = await _db.MenuRole.SingleOrDefaultAsync(x => x.MenuId == id);
                if(hrrole == null)
                {
                    _db.MenuList.Remove(hrmenu);
                    await _db.SaveChangesAsync();
                    return await Result<int>.SuccessAsync(hrmenu.Id, _localizer["Menu Deleted"]);
                }
                else
                {
                    _db.MenuRole.Remove(hrrole);
                    await _db.SaveChangesAsync();
                    _db.MenuList.Remove(hrmenu);
                    await _db.SaveChangesAsync();
                    return await Result<int>.SuccessAsync(hrmenu.Id, _localizer["Menu Deleted"]);
                }
               
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Menu Not Select"]);
            }
            
        }

        public async Task<Result<List<MenuListResponse>>> GetAllAsync()
        {
            Expression<Func<MenuList, MenuListResponse>> expression = e => new MenuListResponse
            {
                Id = e.Id,
                MenuName = e.MenuName,
                MenuNameNepali = e.MenuNameNepali,
                ParentId = e.ParentId,
                Icon = e.Icon,
                Path = e.Path,
                ParentItem = e.ParentItem.MenuName

            };
            var menulist = await _db.MenuList.Select(expression).ToListAsync();
            var hrmenuResponse = _mapper.Map<List<MenuListResponse>>(menulist);
            return await Result<List<MenuListResponse>>.SuccessAsync(hrmenuResponse);
        }

        public async Task<Result<MenuListResponse>> GetByIdAsync(int id)
        {
            var hrmenu = await _db.MenuList.FindAsync(id);
            var hrmenuResponse = _mapper.Map<MenuListResponse>(hrmenu);
            return await Result<MenuListResponse>.SuccessAsync(hrmenuResponse);
        }

        public Task<int> GetCountAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Result<List<MenuListResponse>>> GetParentItemAsync()
        {

            Expression<Func<MenuList, MenuListResponse>> expression = e => new MenuListResponse
            {
                Id=e.Id,
                MenuName=e.MenuName
            };

            var menuList = await _db.MenuList.Select(expression).Distinct().ToListAsync();
            return await Result<List<MenuListResponse>>.SuccessAsync(menuList);
        }

        public async Task<Result<string>> SaveAsync(MenuListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Id.ToString()))
            {
                return await Result<string>.FailAsync(_localizer["Menu required"]);
            }
            if (request.Id == 0)
            {
                if(request.ParentId == 0)
                {
                    _hrmenu.Id = request.Id;
                    _hrmenu.MenuName = request.MenuName;
                    _hrmenu.MenuNameNepali = request.MenuNameNepali;
                    _hrmenu.Icon = request.Icon;
                    _hrmenu.Path = request.Path;
                    var menulist = _mapper.Map<MenuList>(_hrmenu);
                    await _db.MenuList.AddAsync(menulist);
                    await _db.SaveChangesAsync();
                    return await Result<string>.SuccessAsync(menulist.MenuName, _localizer["Menu Created"]);
                }
                else
                {
                    var menulist = _mapper.Map<MenuList>(request);
                    await _db.MenuList.AddAsync(menulist);
                    await _db.SaveChangesAsync();
                    return await Result<string>.SuccessAsync(menulist.MenuName, _localizer["Menu Created"]);
                }
                
            }
            else
            {
                var hrmenu = await _db.MenuList.SingleOrDefaultAsync(x => x.Id == request.Id);
                if (hrmenu.MenuName is not null)
                {
                    hrmenu.MenuName = request.MenuName;
                    hrmenu.MenuNameNepali = request.MenuNameNepali;
                    hrmenu.ParentId = request.ParentId;
                    hrmenu.Path = request.Path;
                    hrmenu.Icon = request.Icon;
                    _db.MenuList.Update(hrmenu);
                    await _db.SaveChangesAsync();
                    return await Result<string>.SuccessAsync(_localizer["Menu Updated"]);
                }
                else
                {
                    return await Result<string>.FailAsync(message: _localizer["Menu not found"]);
                }  
              
            }
        }
    }
}
