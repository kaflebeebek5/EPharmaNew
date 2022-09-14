using AutoMapper;
using EPharma.Application.Interfaces.Repositories;
using EPharma.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EPharma.Application.Features.BankSetup.Queries.GetById
{
    public class GetBankSetupByIdQuery :IRequest<Result<List<GetBankSetupByIdResponse>>>
    {
        public int Id { get; set; }
    }
    internal class GetBankSetupByIdQueryHandler : IRequestHandler<GetBankSetupByIdQuery, Result<List<GetBankSetupByIdResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetBankSetupByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<GetBankSetupByIdResponse>>> Handle(GetBankSetupByIdQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<EPharma.Domain.Entities.Settings.BankSetup, GetBankSetupByIdResponse>> expression = e => new GetBankSetupByIdResponse
            {
                Id = e.Id,
                Name = e.Name
            };
            var banksetup = _unitOfWork.Repository<EPharma.Domain.Entities.Settings.BankSetup>().Entities.Select(expression).Distinct().ToList();
            return await Result<List<GetBankSetupByIdResponse>>.SuccessAsync(banksetup);
            //var bankSetup = await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.BankSetup>().GetByIdAsync(query.Id);
            //var mappedBankSetup = _mapper.Map<GetBankSetupByIdResponse>(bankSetup);
            //return await Result<GetBankSetupByIdResponse>.SuccessAsync(mappedBankSetup);
        }
    }

}
