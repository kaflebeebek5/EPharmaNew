using EPharma.Application.Interfaces.Repositories;
using EPharma.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace EPharma.Application.Features.Branch.Commands.Delete
{
    public class DeleteBranchCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteBranchCommandHandler : IRequestHandler<DeleteBranchCommand, Result<int>>
    {
        private readonly IStringLocalizer<DeleteBranchCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteBranchCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteBranchCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteBranchCommand command, CancellationToken cancellationToken)
        {
            var branch = await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.Branch>().GetByIdAsync(command.Id);
            if (branch != null)
            {
                await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.Branch>().DeleteAsync(branch);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(branch.Id, _localizer["Branch Deleted"]);
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Branch Not Found!"]);
            }
        }
    }
}