using AutoMapper;
using EPharma.Application.Interfaces.Repositories;
using EPharma.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace EPharma.Application.Features.Branch.Commands.AddEdit
{
    public partial class AddEditBranchCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string NameNepali { get; set; }
        public string Code { get; set; }
        public string NRBCode { get; set; }
        public DateTime? OperationDate { get; set; }
        public int? ParentBranchId { get; set; }
        [Required]
        public int? BranchTypeId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? LocalBodiesId { get; set; }
        public string Locality { get; set; }
        public int? WardNo { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNo { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    internal class AddEditBranchCommandHandler : IRequestHandler<AddEditBranchCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditBranchCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditBranchCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditBranchCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditBranchCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var branch = _mapper.Map<EPharma.Domain.Entities.Settings.Branch>(command);
                await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.Branch>().AddAsync(branch);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(branch.Id, _localizer["Branch Saved"]);
            }
            else
            {
                var branch = await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.Branch>().GetByIdAsync(command.Id);
                if (branch != null)
                {
                    branch.Name = command.Name ?? branch.Name;
                    branch.NameNepali = command.NameNepali ?? branch.NameNepali;
                    branch.Code = command.Code ?? branch.Code;
                    branch.NRBCode = command.NRBCode ?? branch.NRBCode;
                    branch.OperationDate = command.OperationDate ?? branch.OperationDate;
                    branch.ParentBranchId = (command.ParentBranchId == 0) ? branch.ParentBranchId : command.ParentBranchId;
                    branch.BranchTypeId = (command.BranchTypeId == 0) ? branch.BranchTypeId : command.BranchTypeId;
                    branch.ProvinceId = (command.ProvinceId == 0) ? branch.ProvinceId : command.ProvinceId;
                    branch.LocalBodiesId = (command.LocalBodiesId == 0) ? branch.LocalBodiesId : command.LocalBodiesId;
                    branch.Locality = command.Locality ?? branch.Locality;
                    branch.WardNo = command.WardNo ?? branch.WardNo;
                    branch.PhoneNo = command.PhoneNo ?? branch.PhoneNo;
                    branch.Email = command.Email ?? branch.Email;

                    await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.Branch>().UpdateAsync(branch);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(branch.Id, _localizer["Branch Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Branch Not Found!"]);
                }
            }
        }
    }
}