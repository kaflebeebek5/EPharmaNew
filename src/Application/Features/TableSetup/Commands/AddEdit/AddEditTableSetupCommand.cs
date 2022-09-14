using System.ComponentModel.DataAnnotations;
using AutoMapper;
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
using System;

namespace EPharma.Application.Features.TableSetup.Commands.AddEdit
{
    public class AddEditTableSetupCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TableName { get; set; }
    }
    internal class AddEditTableSetupCommandHandler : IRequestHandler<AddEditTableSetupCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditTableSetupCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditTableSetupCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditTableSetupCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditTableSetupCommand command, CancellationToken cancellationToken)
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

            if (command.Id == 0)
            {
                var mapperMap = _mapper.GetType().GetMethod("Map", BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly, null, new Type[] { tableClass.Implementation }, null).MakeGenericMethod(tableClass.Implementation);
                var mappedModel = mapperMap.Invoke(_mapper, new object[] { command });
                var addAsyncMethod = repository.GetType().GetMethod("AddAsync");
                var task = (Task)addAsyncMethod.Invoke(repository, new object[] { mappedModel });
                await task.ConfigureAwait(false);
                var taskResult = (task.GetType().GetProperty("Result")).GetValue(task);
                var resultUpdate = taskResult.GetType().GetProperty("Id");
                var result = (int)resultUpdate.GetValue(taskResult);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(result, _localizer["Data Saved"]);
            }
            else
            {
                var getByIdAsyncMethod = repository.GetType().GetMethod("GetByIdAsync");
                var task = (Task)getByIdAsyncMethod.Invoke(repository, new object[] { command.Id });
                await task.ConfigureAwait(false);
                var resultProperty = task.GetType().GetProperty("Result");
                var tableSetup = resultProperty.GetValue(task);
                if (tableSetup != null)
                {
                    var mapperMap = _mapper.GetType().GetMethod("Map", BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly, null, new Type[] { tableClass.Implementation }, null).MakeGenericMethod(tableClass.Implementation);
                    var mappedModel = mapperMap.Invoke(_mapper, new object[] { command });
                    var addAsyncMethod = repository.GetType().GetMethod("UpdateAsync");
                    mappedModel.GetType().GetProperty("Name").SetValue(mappedModel, command.Name ?? mappedModel.GetType().GetProperty("Name").GetValue(mappedModel));
                    var taskUpdate = (Task)addAsyncMethod.Invoke(repository, new object[] { mappedModel });
                    await taskUpdate.ConfigureAwait(false);
                    var taskResult = (task.GetType().GetProperty("Result")).GetValue(task);
                    var resultUpdate = taskResult.GetType().GetProperty("Id");
                    var result = (int)resultUpdate.GetValue(taskResult);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(result, _localizer["Data Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Data Not Found!"]);
                }
            }
        }
    }
}
