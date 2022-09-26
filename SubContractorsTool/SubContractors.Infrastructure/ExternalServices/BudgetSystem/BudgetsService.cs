using Microsoft.Extensions.Logging;
using RestSharp;
using SubContractors.Common.RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SubContractors.Infrastructure.ExternalServices.BudgetSystem.RequestModels;
using SubContractors.Infrastructure.ExternalServices.BudgetSystem.ResponseModels;
using SubContractors.Infrastructure.ExternalServices.BudgetSystem.ResponseModels.BudgetDictionaries;

namespace SubContractors.Infrastructure.ExternalServices.BudgetSystem
{
    public class BudgetsService : IBudgetsService
    {
        private readonly IApplicationExternalQueryHandler _queryHandler;
        private readonly IBudgetsOptions _budgetsOptions;
        private readonly ILogger _logger;

        public BudgetsService(IBudgetsOptions budgetsOptions,
                              ILogger<BudgetsService> logger,
                              IApplicationExternalQueryHandler queryHandler )
        {
            _budgetsOptions = budgetsOptions;
            _queryHandler = queryHandler;
            _logger = logger;
            _queryHandler.SetConfiguration(_budgetsOptions);
        }

        public async Task<RegisterInvoiceResponse> RegisterInvoicesAsync(List<RegisterInvoiceRequest> requests)
        {
            var request = new RestRequest(_budgetsOptions.BaseUrl, Method.Post).AddObject(requests);
            
            var response = await _queryHandler.QueryClient.ExecuteGetAsync(request);

            if (response.IsSuccessful)
            {
                return new RegisterInvoiceResponse
                {
                    Data = response.Content,
                    ErrorCode = (int)response.StatusCode,
                    ErrorMessage = string.Empty,
                    IsError = false,
                }; 
            }

            if (response.ErrorException != null)
            {
                if (!string.IsNullOrEmpty(response.ErrorMessage))
                {
                    _logger.LogError(response.ErrorMessage);
                }
                _logger.LogError(response.ErrorException.InnerException?.Message);
            }

            return new RegisterInvoiceResponse
            {
                Data = null,
                ErrorCode = (int)response.StatusCode,
                ErrorMessage = string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorException?.Message : response.ErrorMessage,
                IsError = true,
            };
        }

        public async Task<BudgetDictionariesResponse> GetBudgetDictionariesAsync()
        {
            var request = new RestRequest(_budgetsOptions.CurrencyAndPaymentMethodsPath);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var response = await _queryHandler.QueryClient.ExecuteGetAsync(request);

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<BudgetDictionariesResponse>(response.Content);
            }

            if (response.ErrorException != null)
            {
                if (!string.IsNullOrEmpty(response.ErrorMessage))
                {
                    _logger.LogError(response.ErrorMessage);
                }
                _logger.LogError(response.ErrorException.InnerException?.Message);
            }

            return new BudgetDictionariesResponse
            {
                Response = new Data(),
                IsError = true,
            };
        }
    }
}
