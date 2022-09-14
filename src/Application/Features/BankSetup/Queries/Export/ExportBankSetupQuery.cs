using EPharma.Application.Extensions;
using EPharma.Application.Interfaces.Repositories;
using EPharma.Application.Interfaces.Services;
using EPharma.Application.Specifications.Settings;
using EPharma.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EPharma.Application.Features.BankSetup.Queries.Export
{
    public class ExportBankSetupQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }
        public ExportBankSetupQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }
        
    internal class ExportBankSetupQueryHandler: IRequestHandler<ExportBankSetupQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportBankSetupQueryHandler> _localizer;

        public ExportBankSetupQueryHandler(IExcelService excelService
            ,IUnitOfWork<int> unitOfWork
            ,IStringLocalizer<ExportBankSetupQueryHandler> localizer)
        {
            _excelService = excelService;
            _localizer = localizer;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> Handle(ExportBankSetupQuery request,CancellationToken cancellationToken)
        {
            var bankSetupFilterSpec = new BankSetupFilterSpecification(request.SearchString);
            var bankSetups = await _unitOfWork.Repository<EPharma.Domain.Entities.Settings.BankSetup>().Entities
                .Specify(bankSetupFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(bankSetups, mappers: new Dictionary<string, Func<EPharma.Domain.Entities.Settings.BankSetup, object>>
            {
                { _localizer["Id"],item=>item.Id},
                {_localizer["Name"],item =>item.Name },
                {_localizer["BranchName"],item=>item.BranchName },
                {_localizer["BankParentId"],item =>item.BankParentId }
            }, sheetName: _localizer["BankSetup"]);
            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
