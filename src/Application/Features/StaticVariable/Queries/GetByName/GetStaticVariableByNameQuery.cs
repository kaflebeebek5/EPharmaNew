using AutoMapper;
using EPharma.Application.Features.StaticVariable.Queries.GetByName;
using EPharma.Application.Interfaces.Repositories;
using EPharma.Shared.Wrapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EPharma.Application.Features.StaticVariables.Queries.GetByName
{
    public class GetStaticVariableByNameQuery : IRequest<Result<GetStaticVariableByNameResponse>>
    {
        public string Name { get; set; }
    }

    internal class GetStaticVariableByNameQueryHandler : IRequestHandler<GetStaticVariableByNameQuery, Result<GetStaticVariableByNameResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetStaticVariableByNameQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetStaticVariableByNameResponse>> Handle(GetStaticVariableByNameQuery query, CancellationToken cancellationToken)
        {
            var staticVariable = await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.StaticVariable>().GetAllAsync();
            var mappedStaticVariable = _mapper.Map<GetStaticVariableByNameResponse>(staticVariable.Where(d => d.Name == query.Name).Select(d => d.Value).FirstOrDefault());
            return await Result<GetStaticVariableByNameResponse>.SuccessAsync(mappedStaticVariable);
        }
    }
}