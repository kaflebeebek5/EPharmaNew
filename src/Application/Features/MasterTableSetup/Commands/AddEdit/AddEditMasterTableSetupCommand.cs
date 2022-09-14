using System.ComponentModel.DataAnnotations;
using AutoMapper;
using EPharma.Application.Interfaces.Repositories;
using EPharma.Domain.Entities.Settings;
using EPharma.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using EPharma.Shared.Constants.Application;

namespace EPharma.Application.Features.MasterTableSetup.Commands.AddEdit
{
    public partial class AddEditMasterTableSetupCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string TableName { get; set; }
        public string ColumnId { get; set; }
        public string ColumnName { get; set; }
    }

    internal class AddEditMasterTableSetupCommandHandler : IRequestHandler<AddEditMasterTableSetupCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditMasterTableSetupCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditMasterTableSetupCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditMasterTableSetupCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditMasterTableSetupCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var masterTableSetup = _mapper.Map<EPharma.Domain.Entities.Settings.MasterTableSetup>(command);
                await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.MasterTableSetup>().AddAsync(masterTableSetup);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllGendersCacheKey);
                return await Result<int>.SuccessAsync(masterTableSetup.Id, _localizer["Table Saved"]);
            }
            else
            {
                var mastertablesetup = await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.MasterTableSetup>().GetByIdAsync(command.Id);
                if (mastertablesetup != null)
                {
                    mastertablesetup.TableName = command.TableName ?? mastertablesetup.TableName;
                    mastertablesetup.ColumnId = command.ColumnId ?? mastertablesetup.ColumnId;
                    mastertablesetup.ColumnName = command.ColumnName ?? mastertablesetup.ColumnName;
                    await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.MasterTableSetup>().UpdateAsync(mastertablesetup);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllGendersCacheKey);
                    return await Result<int>.SuccessAsync(mastertablesetup.Id, _localizer["Table Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Table Not Found!"]);
                }
            }
        }
    }
}
