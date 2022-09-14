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

namespace EPharma.Application.Features.MasterTableSetup.Queries.Export
{
    public class ExportMasterTableSetupQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportMasterTableSetupQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }
    internal class ExportMasterTableSetupQueryHandler : IRequestHandler<ExportMasterTableSetupQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportMasterTableSetupQueryHandler> _localizer;

        public ExportMasterTableSetupQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportMasterTableSetupQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportMasterTableSetupQuery request, CancellationToken cancellationToken)
        {
            var mastertableFilterSpec = new MasterTableSetupFilterSpecification(request.SearchString);
            var mastertable = await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.MasterTableSetup>().Entities
                .Specify(mastertableFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(mastertable, mappers: new Dictionary<string, Func<EPharma.Domain.Entities.Settings.MasterTableSetup, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["TableName"], item => item.TableName },
            }, sheetName: _localizer["MasterTableSetup"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
