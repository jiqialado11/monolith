using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using SubContractors.Common.RestSharp;
using SubContractors.Infrastructure.ExternalServices.PmCoreSystem.ResponseModels.StaffList;

namespace SubContractors.Infrastructure.ExternalServices.PmCoreSystem
{
    public class PmCoreSystemService: IPmCoreSystemService
    {
        private readonly IApplicationExternalQueryHandler _queryHandler;
        private readonly IPmCoreSystemOptions _pmCoreSystemOptions;
        private readonly ILogger _logger;

        public PmCoreSystemService(IApplicationExternalQueryHandler queryHandler,
            IPmCoreSystemOptions pmCoreSystemOptions, 
            ILogger<PmCoreSystemService> logger)
        {
            _queryHandler = queryHandler;
            _pmCoreSystemOptions = pmCoreSystemOptions;
            _logger = logger;
            _queryHandler.SetConfiguration(_pmCoreSystemOptions);
        }

        public async Task<Response> GetStaffListAsync()
        {
            var request = new RestRequest(_pmCoreSystemOptions.StaffListPath);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var response = await _queryHandler.QueryClient.ExecuteGetAsync(request);

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<Response>(response.Content);
            }

            if (response.ErrorException != null)
            {
                if (!string.IsNullOrEmpty(response.ErrorMessage))
                {
                    _logger.LogError(response.ErrorMessage);
                }
                _logger.LogError(response.ErrorException.InnerException?.Message);
            }
            return new Response
            {
                StaffContractorList = null,
                ErrorMessage = string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorException?.Message : response.ErrorMessage,
                IsError = true,
            };
        }

        public async Task<ResponseModels.StaffDetails.Response> GetStaffDetailsAsync(int staffId)
        {
            var request = new RestRequest(_pmCoreSystemOptions.StaffDetailsPath).AddParameter(nameof(staffId),staffId);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var response = await _queryHandler.QueryClient.ExecuteGetAsync(request);
            
            if (response.IsSuccessful)
            {
                return new ResponseModels.StaffDetails.Response
                {
                    Data = JsonConvert.DeserializeObject<ResponseModels.StaffDetails.Data>(response.Content),
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

            return new ResponseModels.StaffDetails.Response
            {
                Data = null,
                ErrorMessage = string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorException?.Message : response.ErrorMessage,
                IsError = true,
            };
        }

        public async Task<ResponseModels.ProjectList.Response> GetProjectListAsync()
        {
            var request = new RestRequest(_pmCoreSystemOptions.ProjectListPath);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var response = await _queryHandler.QueryClient.ExecuteGetAsync(request);

            if (response.IsSuccessful)
            {
                return new ResponseModels.ProjectList.Response
                {
                    Data = JsonConvert.DeserializeObject<List<ResponseModels.ProjectList.Project>>(response.Content),
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
            return new ResponseModels.ProjectList.Response
            {
                Data = null,
                ErrorMessage = string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorException?.Message : response.ErrorMessage,
                IsError = true,
            };
        }

        public async Task<ResponseModels.ProjectDetails.Response> GetProjectDetailsAsync(int projectId)
        {
            var request = new RestRequest(_pmCoreSystemOptions.ProjectDetailsPath).AddParameter(nameof(projectId), projectId);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var response = await _queryHandler.QueryClient.ExecuteGetAsync(request);

            if (response.IsSuccessful)
            {
                return new ResponseModels.ProjectDetails.Response
                {
                    Project = JsonConvert.DeserializeObject<ResponseModels.ProjectDetails.Project>(response.Content),
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

            return new ResponseModels.ProjectDetails.Response
            {
                Project = null,
                ErrorMessage = string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorException?.Message : response.ErrorMessage,
                IsError = true,
            };
        }

        public async Task<ResponseModels.DetailStaffList.Response> GetStaffListPerLastDaysAsync(int days)
        {
            var request = new RestRequest(_pmCoreSystemOptions.NewlyAddedStaffList).AddParameter(nameof(days), days);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var response = await _queryHandler.QueryClient.ExecuteGetAsync(request);

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<ResponseModels.DetailStaffList.Response>(response.Content);
            }

            if (response.ErrorException != null)
            {
                if (!string.IsNullOrEmpty(response.ErrorMessage))
                {
                    _logger.LogError(response.ErrorMessage);
                }
                _logger.LogError(response.ErrorException.InnerException?.Message);
            }
            return new ResponseModels.DetailStaffList.Response
            {
                StaffList = new List<ResponseModels.DetailStaffList.Staff>(),
                ErrorMessage = string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorException?.Message : response.ErrorMessage,
                IsError = true,
            };
        }
    }
}
