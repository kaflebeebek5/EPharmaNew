using AutoMapper;
using EPharma.Application.Interfaces.Repositories;
using EPharma.Domain.Entities.Settings;
using EPharma.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EPharma.Application.Features.Vdc.Queries.GetById
{
    public class GetVdcByIdQuery : IRequest<Result<List<GetVdcByIdResponse>>>
    {
        public int Id { get; set; }
    }
    internal class GetVdcByIdQueryHandler : IRequestHandler<GetVdcByIdQuery, Result<List<GetVdcByIdResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetVdcByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetVdcByIdResponse>>> Handle(GetVdcByIdQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<LocalBodies, GetVdcByIdResponse>> expression = e => new GetVdcByIdResponse
            {
                Id = e.Id,
                Name = e.Name,
                DistrictId = e.DistrictId
            };
            var mappedvdc= _unitOfWork.Repository<LocalBodies>().Entities.Select(expression).Where(p => p.DistrictId == query.Id).ToList();
            return await Result<List<GetVdcByIdResponse>>.SuccessAsync(mappedvdc);
        }
    }
}
