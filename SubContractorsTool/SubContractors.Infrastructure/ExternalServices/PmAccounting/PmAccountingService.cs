using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using SubContractors.Common.RestSharp;
using SubContractors.Infrastructure.ExternalServices.PmAccounting.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SubContractors.Infrastructure.ExternalServices.PmAccounting
{
    public class PmAccountingService : IPmAccountingService
    {
        private readonly IApplicationExternalQueryHandler _queryHandler;
        private readonly IPmAccountingOptions _pmAccountingOptions;
        private readonly ILogger _logger;
        public PmAccountingService(IApplicationExternalQueryHandler queryHandler,
                                   IPmAccountingOptions pmAccountingOptions,
                                   ILogger<PmAccountingService> logger)
        {
            _logger = logger;
            _queryHandler = queryHandler;
            _pmAccountingOptions = pmAccountingOptions;
            _queryHandler.SetConfiguration(pmAccountingOptions);
        }

        public async Task<PmAccountingResponse> GetMilestonesAsync(int projectId)
        {
            var request = new RestRequest(_pmAccountingOptions.MilestonesPath).AddParameter(nameof(projectId), projectId);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var response = await _queryHandler.QueryClient.ExecuteGetAsync(request);

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<PmAccountingResponse>(response.Content);
            }

            if (response.ErrorException != null)
            {
                if (!string.IsNullOrEmpty(response.ErrorMessage))
                {
                    _logger.LogError(response.ErrorMessage);
                }
                _logger.LogError(response.ErrorException.InnerException?.Message);
            }
            var data = new Data();
            data.Milestones = new List<Milestone>();
            return new PmAccountingResponse
            {
                Data = data,
                ErrorCode = (int)response.StatusCode,
                ErrorMessage = string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorException?.Message : response.ErrorMessage,
                IsError = true,
            };
        }
    }
}
