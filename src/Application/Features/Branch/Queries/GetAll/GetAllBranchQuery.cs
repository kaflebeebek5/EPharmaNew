using AutoMapper;
using EPharma.Application.Interfaces.Repositories;
using EPharma.Shared.Constants.Application;
using EPharma.Shared.Wrapper;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EPharma.Application.Features.Branch.Queries.GetAll
{
    public class GetAllBranchQuery : IRequest<Result<List<GetAllBranchResponse>>>
    {
        public GetAllBranchQuery()
        {
        }
    }

    internal class GetAllBranchCachedQueryHandler : IRequestHandler<GetAllBranchQuery, Result<List<GetAllBranchResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllBranchCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllBranchResponse>>> Handle(GetAllBranchQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<EPharma.Domain.Entities.Settings.Branch, GetAllBranchResponse>> expression = e => new GetAllBranchResponse
            {
                Id = e.Id,
                Name =  e.Name,
                NameNepali =  e.NameNepali,
                Code =  e.Code,
                NRBCode =  e.NRBCode,
                OperationDate =  e.OperationDate,
                ParentBranchId =  e.ParentBranchId,
                ParentBranch =  e.ParentBranch.Name,
                BranchTypeId =  e.BranchTypeId,
                BranchType =  e.BranchType.Name,
                ProvinceId =  e.ProvinceId,
                Province =  e.Province.Name,
                DistrictId =  e.DistrictId,
                LocalBodiesId =  e.LocalBodiesId,
                LocalBodies =  e.LocalBodies.Name,
                Locality = e.Locality,
                WardNo = e.WardNo,
                PhoneNo = e.PhoneNo,
                Email = e.Email
            };
            var mappedBranch = await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.Branch>().Entities.Select(expression).ToListAsync();
            return await Result<List<GetAllBranchResponse>>.SuccessAsync(mappedBranch);
        }
    }
}