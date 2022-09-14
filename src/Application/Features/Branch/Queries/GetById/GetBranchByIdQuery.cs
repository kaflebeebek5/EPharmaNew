using AutoMapper;
using EPharma.Application.Features.Branch.Queries.GetAll;
using EPharma.Application.Interfaces.Repositories;
using EPharma.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EPharma.Application.Features.Branch.Queries.GetById
{
    public class GetBranchByIdQuery : IRequest<Result<GetBranchByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetBranchByIdQueryHandler : IRequestHandler<GetBranchByIdQuery, Result<GetBranchByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetBranchByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetBranchByIdResponse>> Handle(GetBranchByIdQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<EPharma.Domain.Entities.Settings.Branch, GetBranchByIdResponse>> expression = e => new GetBranchByIdResponse
            {
                Id = e.Id,
                Name = e.Name,
                NameNepali = e.NameNepali,
                Code = e.Code,
                NRBCode = e.NRBCode,
                OperationDate = e.OperationDate,
                ParentBranchId = e.ParentBranchId,
                ParentBranch = e.ParentBranch.Name,
                BranchTypeId = e.BranchTypeId,
                BranchType = e.BranchType.Name,
                ProvinceId = e.ProvinceId,
                Province = e.Province.Name,
                DistrictId = e.DistrictId,
                LocalBodiesId = e.LocalBodiesId,
                LocalBodies = e.LocalBodies.Name,
                Locality = e.Locality,
                WardNo = e.WardNo,
                PhoneNo = e.PhoneNo,
                Email = e.Email
            };
            var mappedBranch = _unitOfWork.Repository<EPharma.Domain.Entities.Settings.Branch>().Entities.Select(expression).Where(p => p.Id==query.Id).FirstOrDefault();
            return await Result<GetBranchByIdResponse>.SuccessAsync(mappedBranch);
        }
    }
}