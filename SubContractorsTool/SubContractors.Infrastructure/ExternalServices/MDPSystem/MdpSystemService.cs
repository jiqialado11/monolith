using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using SubContractors.Common.RestSharp;
using SubContractors.Infrastructure.ExternalServices.MDPSystem.RequestModels.RegisterVendor;
using SubContractors.Infrastructure.ExternalServices.MDPSystem.ResponseModels.ContractorData;
using SubContractors.Infrastructure.ExternalServices.MDPSystem.ResponseModels.LegalEntityData;
using SubContractors.Infrastructure.ExternalServices.MDPSystem.ResponseModels.LocationData;
using SubContractors.Infrastructure.ExternalServices.MDPSystem.ResponseModels.VendorData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SubContractors.Infrastructure.ExternalServices.MDPSystem
{
    public class MdpSystemService : IMdpSystemService
    {
        private readonly IApplicationExternalQueryHandler _queryHandler;
        private readonly IMdpSystemOptions _mdpOptions;
        private ILogger _logger;
        public MdpSystemService(IMdpSystemOptions options,
                                IApplicationExternalQueryHandler queryHandler,
                                ILogger<MdpSystemService> logger)                   
        {
            _mdpOptions = options;
            _queryHandler = queryHandler;
            _queryHandler.SetConfiguration(_mdpOptions);
            _logger = logger;
        }

        public async Task<ContractorResponse> GetContractorsAsync()
        {
            var request = new RestRequest(_mdpOptions.ContractorsPath);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var response = await _queryHandler.QueryClient.ExecuteGetAsync(request);

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<ContractorResponse>(response.Content);
            }

            if (response.ErrorException != null)
            {
                if (!string.IsNullOrEmpty(response.ErrorMessage))
                {
                    _logger.LogError(response.ErrorMessage);
                }
                _logger.LogError(response.ErrorException.InnerException?.Message);
            }

            var data = new List<ContractorMdp>();
            return new ContractorResponse
            {
                Data = data,
                ErrorCode = (int)response.StatusCode,
                ErrorMessage = string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorException?.Message : response.ErrorMessage,
                IsError = true,
            };
        }

        public async Task<LegalEntityResponse> GetLegalEntitiesAsync()
        {
            var EnglishName = string.Empty;
            var request = new RestRequest(_mdpOptions.LegalEntitiesPath).AddParameter(nameof(EnglishName), EnglishName);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var response = await _queryHandler.QueryClient.ExecuteGetAsync(request);

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<LegalEntityResponse>(response.Content);
            }

            if (response.ErrorException != null)
            {
                if (!string.IsNullOrEmpty(response.ErrorMessage))
                {
                    _logger.LogError(response.ErrorMessage);
                }
                _logger.LogError(response.ErrorException.InnerException?.Message);
            }

            var data = new List<LegalEntityMdp>();
            return new LegalEntityResponse
            {
                Data = data,
                ErrorCode = (int)response.StatusCode,
                ErrorMessage = string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorException?.Message : response.ErrorMessage,
                IsError = true,
            };
        }

        public async Task<LocationsResponse> GetLocationsAsync()
        {
            var EnglishName = string.Empty;
            var request = new RestRequest(_mdpOptions.LocationsPath).AddParameter(nameof(EnglishName), EnglishName);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var response = await _queryHandler.QueryClient.ExecuteGetAsync(request);

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<LocationsResponse>(response.Content);
            }

            if (response.ErrorException != null)
            {
                if (!string.IsNullOrEmpty(response.ErrorMessage))
                {
                    _logger.LogError(response.ErrorMessage);
                }
                _logger.LogError(response.ErrorException.InnerException?.Message);
            }

            var data = new List<LocationMdp>();
            return new LocationsResponse
            {
                Locations = data,
                ErrorCode = (int)response.StatusCode,
                ErrorMessage = string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorException?.Message : response.ErrorMessage,
                IsError = true,
            };
        }

        public async Task<VendorsResponse> GetSubcontractors()
        {
            var request = new RestRequest(_mdpOptions.VendorsPath);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var response = await _queryHandler.QueryClient.ExecuteGetAsync(request);

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<VendorsResponse>(response.Content);
            }

            if (response.ErrorException != null)
            {
                if (!string.IsNullOrEmpty(response.ErrorMessage))
                {
                    _logger.LogError(response.ErrorMessage);
                }
                _logger.LogError(response.ErrorException.InnerException?.Message);
            }

            var data = new List<VendorMdp>();
            return new VendorsResponse
            {
                Data = data,
                ErrorCode = (int)response.StatusCode,
                ErrorMessage = string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorException?.Message : response.ErrorMessage,
                IsError = true,
            };
        }

        public async Task<PostVendorResponse> PostVendorAsync(Vendor vendorRequestModel)
        {
            var request = new RestRequest(_mdpOptions.BaseUrl, Method.Post).AddObject(vendorRequestModel);
            var response = await _queryHandler.QueryClient.ExecuteGetAsync(request);

            if (response.IsSuccessful)
            {
                return new PostVendorResponse
                {
                    ErrorCode = (int)response.StatusCode,
                    IsSuccess = true,
                    Message = String.Empty
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

            return new PostVendorResponse
            {
                ErrorCode = (int)response.StatusCode,
                Message = string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorException?.Message : response.ErrorMessage.ToString(),
                IsError = true,
            };
        }
    }
}
