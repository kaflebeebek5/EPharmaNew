using EPharma.Application.Interfaces.Repositories;
using EPharma.Domain.Entities.Settings;
using EPharma.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using EPharma.Shared.Constants.Application;

namespace EPharma.Application.Features.Genders.Commands.Delete
{
    public class DeleteGenderCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteGenderCommandHandler : IRequestHandler<DeleteGenderCommand, Result<int>>
    {
        private readonly IStringLocalizer<DeleteGenderCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteGenderCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteGenderCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteGenderCommand command, CancellationToken cancellationToken)
        {
            var gender = await _unitOfWork.Repository<Gender>().GetByIdAsync(command.Id);
            if (gender != null)
            {
                await _unitOfWork.Repository<Gender>().DeleteAsync(gender);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllGendersCacheKey);
                return await Result<int>.SuccessAsync(gender.Id, _localizer["Gender Deleted"]);
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Gender Not Found!"]);
            }
        }
    }
}