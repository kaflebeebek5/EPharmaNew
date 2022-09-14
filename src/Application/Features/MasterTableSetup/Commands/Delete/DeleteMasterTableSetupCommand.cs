using EPharma.Application.Interfaces.Repositories;
using EPharma.Domain.Entities.Settings;
using EPharma.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using EPharma.Shared.Constants.Application;

namespace EPharma.Application.Features.MasterTableSetup.Commands.Delete
{
    public class DeleteMasterTableSetupCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }
    internal class DeleteMasterTableSetupCommandHandler : IRequestHandler<DeleteMasterTableSetupCommand, Result<int>>
    {
        private readonly IStringLocalizer<DeleteMasterTableSetupCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteMasterTableSetupCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteMasterTableSetupCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteMasterTableSetupCommand command, CancellationToken cancellationToken)
        {
            var mastertablesetup = await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.MasterTableSetup>().GetByIdAsync(command.Id);
            if (mastertablesetup != null)
            {
                await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.MasterTableSetup>().DeleteAsync(mastertablesetup);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllGendersCacheKey);
                return await Result<int>.SuccessAsync(mastertablesetup.Id, _localizer["Table Deleted"]);
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Table Not Found!"]);
            }
        }
    }
}
