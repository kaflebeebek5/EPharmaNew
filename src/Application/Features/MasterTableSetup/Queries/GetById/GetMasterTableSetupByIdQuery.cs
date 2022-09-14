using AutoMapper;
using EPharma.Application.Interfaces.Repositories;
using EPharma.Domain.Entities.Settings;
using EPharma.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EPharma.Application.Features.MasterTableSetup.Queries.GetById
{
    public class GetMasterTableSetupByIdQuery : IRequest<Result<GetMasterTableSetupByIdResponse>>
    {
        public int Id { get; set; }
    }
    internal class GetMasterTableSetupByIdQueryHandler : IRequestHandler<GetMasterTableSetupByIdQuery, Result<GetMasterTableSetupByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetMasterTableSetupByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetMasterTableSetupByIdResponse>> Handle(GetMasterTableSetupByIdQuery query, CancellationToken cancellationToken)
        {
            var mastertable = await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.MasterTableSetup>().GetByIdAsync(query.Id);
            var mappedmastertable = _mapper.Map<GetMasterTableSetupByIdResponse>(mastertable);
            return await Result<GetMasterTableSetupByIdResponse>.SuccessAsync(mappedmastertable);
        }
    }
}
