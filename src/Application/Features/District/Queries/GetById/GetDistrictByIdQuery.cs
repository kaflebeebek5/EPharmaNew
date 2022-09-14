using AutoMapper;
using EPharma.Application.Interfaces.Repositories;
using EPharma.Domain.Entities.Settings;
using EPharma.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EPharma.Application.Features.Province.Queries.GetById
{
    public class GetDistrictByIdQuery : IRequest<Result<List<GetDistrictByIdResponse>>>
    {
        public int Id { get; set; }
    }
    internal class GetProvinceByIdQueryHandler : IRequestHandler<GetDistrictByIdQuery, Result<List<GetDistrictByIdResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetProvinceByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetDistrictByIdResponse>>> Handle(GetDistrictByIdQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<District, GetDistrictByIdResponse>> expression = e => new GetDistrictByIdResponse
            {
                Id=e.Id,
                Name=e.Name,
                ProvinceId=e.ProvinceId
            };
            var mappeddistrict = _unitOfWork.Repository<District>().Entities.Select(expression).Where(p => p.ProvinceId == query.Id).ToList();
            //var district = await _unitOfWork.Repository<District>().GetByIdAsync(query.Id);
            //var mappeddistrict = _mapper.Map<GetDistrictByIdResponse>(district);
            return await Result<List<GetDistrictByIdResponse>>.SuccessAsync(mappeddistrict);
        }
    }
}
