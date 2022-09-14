using AutoMapper;
using EPharma.Application.Interfaces.Repositories;
using EPharma.Shared.Wrapper;
using LazyCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EPharma.Application.Features.StaticVariable.Queries.GetAll
{
    public class GetAllStaticVariableQuery : IRequest<Result<List<GetAllStaticVariableResponse>>>
    {
        public GetAllStaticVariableQuery()
        {
        }
    }

    internal class GetAllStaticVariableQueryHandler : IRequestHandler<GetAllStaticVariableQuery, Result<List<GetAllStaticVariableResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllStaticVariableQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllStaticVariableResponse>>> Handle(GetAllStaticVariableQuery request, CancellationToken cancellationToken)
        {
            var staticVariables = await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.StaticVariable>().GetAllAsync();
            var mappedStaticVariables = _mapper.Map<List<GetAllStaticVariableResponse>>(staticVariables);
            return await Result<List<GetAllStaticVariableResponse>>.SuccessAsync(mappedStaticVariables);
        }
    }
}