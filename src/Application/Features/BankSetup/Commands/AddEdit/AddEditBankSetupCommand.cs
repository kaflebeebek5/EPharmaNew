using AutoMapper;
using EPharma.Application.Interfaces.Repositories;
using EPharma.Shared.Constants.Application;
using EPharma.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

using System.Threading;
using System.Threading.Tasks;

namespace EPharma.Application.Features.BankSetup.Commands.AddEdit
{
    public partial class AddEditBankSetupCommand :IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string BranchName { get; set; }
        public int? BankParentId { get; set; }
    }

    internal class AddEditBankSetupCommandHandler: IRequestHandler<AddEditBankSetupCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditBankSetupCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public  AddEditBankSetupCommandHandler(IUnitOfWork<int> unitOfWork,IMapper mapper,IStringLocalizer<AddEditBankSetupCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }
        public async Task<Result<int>> Handle(AddEditBankSetupCommand command, CancellationToken cancellationToken)
        {
            if(command.Id == 0)
            {
                var bankSetup = _mapper.Map<EPharma.Domain.Entities.Settings.BankSetup>(command);
                await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.BankSetup>().AddAsync(bankSetup);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllBankSetupCacheKey);
                return await Result<int>.SuccessAsync(bankSetup.Id, _localizer["BankSetup Saved"]);
            }
            else
            {
                var bankSetup = await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.BankSetup>().GetByIdAsync(command.Id);
                if(bankSetup !=null)
                {
                    bankSetup.Name = command.Name ?? bankSetup.Name;
                    bankSetup.BranchName = command.BranchName ?? bankSetup.BranchName;
                    await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.BankSetup>().UpdateAsync(bankSetup);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllBankSetupCacheKey);
                    return await Result<int>.SuccessAsync(bankSetup.Id, _localizer["BankSetup Updated For {0}({1})", bankSetup.Name,bankSetup.BranchName]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["BankSetup Not Found"]);
                }
            }
        }
    }
}
