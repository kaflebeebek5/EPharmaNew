using AutoMapper;
using EPharma.Application.Interfaces.Repositories;
using EPharma.Domain.Entities.Settings;
using EPharma.Shared.Constants.Application;
using EPharma.Shared.Wrapper;
using LazyCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EPharma.Application.Features.MasterTableSetup.Queries.GetAll
{
    public class GetAllMasterTableSetupQuery : IRequest<Result<List<GetAllMasterTableSetupResponse>>>
    {
        public GetAllMasterTableSetupQuery()
        {
        }
    }
    internal class GetAllMasterTableSetupCachedQueryHandler : IRequestHandler<GetAllMasterTableSetupQuery, Result<List<GetAllMasterTableSetupResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllMasterTableSetupCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllMasterTableSetupResponse>>> Handle(GetAllMasterTableSetupQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<EPharma.Domain.Entities.Settings.MasterTableSetup>>> getAllGenders = () => _unitOfWork.Repository<EPharma.Domain.Entities.Settings.MasterTableSetup>().GetAllAsync();
            List<EPharma.Domain.Entities.Settings.MasterTableSetup> genderList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllGendersCacheKey, getAllGenders);
            var mappedGenders = _mapper.Map<List<GetAllMasterTableSetupResponse>>(genderList);
            return await Result<List<GetAllMasterTableSetupResponse>>.SuccessAsync(mappedGenders);
        }
    }
}
