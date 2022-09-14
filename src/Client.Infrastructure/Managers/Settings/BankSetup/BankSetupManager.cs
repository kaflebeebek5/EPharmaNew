using EPharma.Application.Features.BankSetup.Commands.AddEdit;
using EPharma.Application.Features.BankSetup.Queries.GetAll;
using EPharma.Application.Features.BankSetup.Queries.GetById;
using EPharma.Client.Infrastructure.Extensions;
using EPharma.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.Settings.BankSetup
{
    public class BankSetupManager : IBankSetupManager
    {
        private readonly HttpClient _httpClient;
        public BankSetupManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.BankSetupEndPoints.Export
                : Routes.BankSetupEndPoints.ExportFilterd(searchString));
            return await response.ToResult<string>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.BankSetupEndPoints.Delete}/{id}");
            return await response.ToResult<int>();
        }
        public async Task<IResult<List<GetAllBankSetupResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.BankSetupEndPoints.GetAll);
            return await response.ToResult<List<GetAllBankSetupResponse>>();
        }
        public async Task<IResult<int>> SaveAsync(AddEditBankSetupCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.BankSetupEndPoints.Save, request);
            return await response.ToResult<int>();
        }
        public async Task<Result<List<GetBankSetupByIdResponse>>> GetParentItemAsync()
        {
            var response = await _httpClient.GetAsync(Routes.BankSetupEndPoints.GetAll);
            //var response = await _httpClient.GetAsync($"{Routes.BankSetupEndPoints.GetParentItem}/ParentItem");
            return (Result<List<GetBankSetupByIdResponse>>)await response.ToResult<List<GetBankSetupByIdResponse>>();
        }
    }
}
