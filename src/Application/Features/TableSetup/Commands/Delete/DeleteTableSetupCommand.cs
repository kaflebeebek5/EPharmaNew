using EPharma.Application.Interfaces.Repositories;
using EPharma.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using EPharma.Shared.Constants.Application;
using EPharma.Domain.Contracts;
using System.Linq;
using System.Reflection;

namespace EPharma.Application.Features.TableSetup.Commands.Delete
{
    public class DeleteTableSetupCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string TableName { get; set; }
    }
    internal class DeleteTableSetupCommandHandler : IRequestHandler<DeleteTableSetupCommand, Result<int>>
    {
        private readonly IStringLocalizer<DeleteTableSetupCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteTableSetupCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteTableSetupCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteTableSetupCommand command, CancellationToken cancellationToken)
        {
            var managers = typeof(AuditableEntity<int>);
            var tableClass = managers
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name == command.TableName)
                .Select(t => new
                {
                    Implementation = t
                }).FirstOrDefault();

            MethodInfo repositoryMethod = _unitOfWork.GetType().GetMethod("Repository").MakeGenericMethod(tableClass.Implementation);
            var repository = repositoryMethod.Invoke(_unitOfWork, null);

            var getByIdAsyncMethod = repository.GetType().GetMethod("GetByIdAsync");
            var task = (Task)getByIdAsyncMethod.Invoke(repository, new object[] { command.Id });
            await task.ConfigureAwait(false);
            var resultProperty = task.GetType().GetProperty("Result");
            var tableSetup = resultProperty.GetValue(task);
            if (tableSetup != null)
            {
                var deleteAsyncMethod = repository.GetType().GetMethod("DeleteAsync");
                var taskDel = (Task)deleteAsyncMethod.Invoke(repository, new object[] { tableSetup });
                await taskDel.ConfigureAwait(false);
                var taskResult = (task.GetType().GetProperty("Result")).GetValue(task);
                var resultUpdate = taskResult.GetType().GetProperty("Id");
                var result = (int)resultUpdate.GetValue(taskResult);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(result, _localizer["Data Deleted"]);
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Data Not Found!"]);
            }
        }
    }
}
