using AutoMapper;
using EPharma.Application.Interfaces.Repositories;
using EPharma.Shared.Constants.Application;
using EPharma.Shared.Wrapper;
using LazyCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EPharma.Application.Features.BankSetup.Queries.GetAll
{
    public class GetAllBankSetupQuery : IRequest<Result<List<GetAllBankSetupResponse>>>
    {
        public GetAllBankSetupQuery()
        {

        }
    }
    internal class GetAllbankSetupCachedQueryHandler : IRequestHandler<GetAllBankSetupQuery, Result<List<GetAllBankSetupResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllbankSetupCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllBankSetupResponse>>> Handle(GetAllBankSetupQuery request,CancellationToken cancellationToken)
        {
            Expression<Func<EPharma.Domain.Entities.Settings.BankSetup, GetAllBankSetupResponse>> expression = e => new GetAllBankSetupResponse
            {
                Id = e.Id,
                Name = e.Name,
                BranchName = e.BranchName,
                BankParentId = e.BankParentId,
                ParentItem = e.ParentItem.Name

            };
            var banksetup = _unitOfWork.Repository<EPharma.Domain.Entities.Settings.BankSetup>().Entities.Select(expression).ToList();
            return await Result<List<GetAllBankSetupResponse>>.SuccessAsync(banksetup);
            //Func<Task<List<EPharma.Domain.Entities.Settings.BankSetup>>> getAllBankSetup = () => _unitOfWork.Repository<EPharma.Domain.Entities.Settings.BankSetup>().GetAllAsync();
            //List<EPharma.Domain.Entities.Settings.BankSetup> bankSetupList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllBankSetupCacheKey, getAllBankSetup);
            //var mappedBankSetup = _mapper.Map<List<GetAllBankSetupResponse>>(bankSetupList);
            //return await Result<List<GetAllBankSetupResponse>>.SuccessAsync(mappedBankSetup);
        }

    }
}
