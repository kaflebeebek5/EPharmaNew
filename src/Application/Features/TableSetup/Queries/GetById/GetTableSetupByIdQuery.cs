using AutoMapper;
using EPharma.Application.Interfaces.Repositories;
using EPharma.Domain.Contracts;
using EPharma.Domain.Entities.Settings;
using EPharma.Shared.Wrapper;
using MediatR;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace EPharma.Application.Features.TableSetup.Queries.GetById
{
    public class GetTableSetupByIdQuery : IRequest<Result<GetTableSetupByIdResponse>>
    {
        public int Id { get; set; }
        public string TableName { get; set; }
    }
    internal class GetTableSetupByIdQueryHandler : IRequestHandler<GetTableSetupByIdQuery, Result<GetTableSetupByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetTableSetupByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetTableSetupByIdResponse>> Handle(GetTableSetupByIdQuery query, CancellationToken cancellationToken)
        {
            var managers = typeof(AuditableEntity<int>);
            var tableClass = managers
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name == query.TableName)
                .Select(t => new
                {
                    Implementation = t
                }).FirstOrDefault();

            MethodInfo repositoryMethod = _unitOfWork.GetType().GetMethod("Repository").MakeGenericMethod(tableClass.Implementation);
            var repository = repositoryMethod.Invoke(_unitOfWork, null);
            var getByIdAsyncMethod = repository.GetType().GetMethod("GetByIdAsync");
            var task = (Task)getByIdAsyncMethod.Invoke(repository, new object[] { query.Id });

            await task.ConfigureAwait(false);
            var resultProperty = task.GetType().GetProperty("Result");
            var allData = resultProperty.GetValue(task);
            var mappedData = _mapper.Map<GetTableSetupByIdResponse>(allData);
            return await Result<GetTableSetupByIdResponse>.SuccessAsync(mappedData);
        }
    }
}
