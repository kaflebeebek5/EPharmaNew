using AutoMapper;
using EPharma.Application.Interfaces.Repositories;
using EPharma.Domain.Contracts;
using EPharma.Domain.Entities.Settings;
using EPharma.Shared.Constants.Application;
using EPharma.Shared.Wrapper;
using LazyCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace EPharma.Application.Features.TableSetup.Queries.GetAll
{
    public class GetAllTableSetupQuery : IRequest<Result<List<GetAllTableSetupResponse>>>
    {
        public string TableName { get; set; }
    }

    internal class GetAllTableSetupCachedQueryHandler : IRequestHandler<GetAllTableSetupQuery, Result<List<GetAllTableSetupResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllTableSetupCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllTableSetupResponse>>> Handle(GetAllTableSetupQuery request, CancellationToken cancellationToken)
        {
            var managers = typeof(AuditableEntity<int>);
            var tableClass = managers
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name == request.TableName)
                .Select(t => new
                {
                    Implementation = t
                }).FirstOrDefault();

            MethodInfo repositoryMethod = _unitOfWork.GetType().GetMethod("Repository").MakeGenericMethod(tableClass.Implementation);
            var repository = repositoryMethod.Invoke(_unitOfWork, null);
            var getAllSyncMethod = repository.GetType().GetMethod("GetAllAsync");
            var task = (Task)getAllSyncMethod.Invoke(repository, null);

            await task.ConfigureAwait(false);
            var resultProperty = task.GetType().GetProperty("Result");
            var allData = resultProperty.GetValue(task);
            var mappedData = _mapper.Map<List<GetAllTableSetupResponse>>(allData);
            return await Result<List<GetAllTableSetupResponse>>.SuccessAsync(mappedData);
        }
    }
}
