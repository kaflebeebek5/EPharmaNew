using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EPharma.Application.Extensions;
using EPharma.Application.Interfaces.Repositories;
using EPharma.Application.Interfaces.Services;
using EPharma.Application.Specifications.Settings;
using EPharma.Domain.Entities.Settings;
using EPharma.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EPharma.Application.Features.Branch.Queries.Export
{
    public class ExportBranchQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportBranchQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportBranchQueryHandler : IRequestHandler<ExportBranchQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportBranchQueryHandler> _localizer;

        public ExportBranchQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportBranchQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportBranchQuery request, CancellationToken cancellationToken)
        {
            var branchFilterSpec = new BranchFilterSpecification(request.SearchString);
            var branch = await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.Branch>().Entities
                .Specify(branchFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(branch, mappers: new Dictionary<string, Func<EPharma.Domain.Entities.Settings.Branch, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Name"], item => item.Name },
                { _localizer["Name Nepali"], item => item.NameNepali },
                { _localizer["Code"], item => item.Code },
                { _localizer["NRB Code"], item => item.NRBCode },
                { _localizer["Operation Date"], item => item.OperationDate },
                { _localizer["Parent Branch"], item => item.ParentBranch.Name },
                { _localizer["BranchType"], item => item.BranchType.Name },
                { _localizer["Province"], item => item.Province.Name },
                { _localizer["District"], item => item.District.Name },
                { _localizer["LocalBodies"], item => item.LocalBodies.Name },
                { _localizer["Locality"], item => item.Locality },
                { _localizer["Ward No"], item => item.WardNo },
                { _localizer["Phone No"], item => item.PhoneNo },
                { _localizer["Email"], item => item.Email },
            }, sheetName: _localizer["Branch"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
