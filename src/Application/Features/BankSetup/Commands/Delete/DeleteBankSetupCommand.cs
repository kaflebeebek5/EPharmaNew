using EPharma.Application.Interfaces.Repositories;
using EPharma.Shared.Constants.Application;
using EPharma.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EPharma.Application.Features.BankSetup.Commands.Delete
{
    public class DeleteBankSetupCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }
    internal class DeleteBankSetupCommandHandler:IRequestHandler<DeleteBankSetupCommand, Result<int>>
    {
        private readonly IStringLocalizer<DeleteBankSetupCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteBankSetupCommandHandler(IUnitOfWork<int> unitOfWork,IStringLocalizer<DeleteBankSetupCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }
        public async Task<Result<int>>Handle (DeleteBankSetupCommand command, CancellationToken cancellationToken)
        {
            var bankSetup = await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.BankSetup>().GetByIdAsync(command.Id);
            if(bankSetup !=null)
            {
                await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.BankSetup>().DeleteAsync(bankSetup);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllBankSetupCacheKey);
                return await Result<int>.SuccessAsync(bankSetup.Id, _localizer["BankSetup Deleted For {0}({1})",bankSetup.Name,bankSetup.BranchName]);
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["BankSetup Not Found"]);
            }
        }
    }
}
