using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.Common;
using SubContractors.Infrastructure.BackgroundJobs.Interfaces;
using SubContractors.Infrastructure.ExternalServices.MDPSystem;
using SubContractors.Infrastructure.ExternalServices.MDPSystem.ResponseModels.LocationData;

namespace SubContractors.Infrastructure.BackgroundJobs.Jobs
{
    public class LocationJobService : ILocationJobService
    {
        private readonly ISqlRepository<Location, int> _locationSqlRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMdpSystemService _mdpSystemService;
        private readonly ILogger<LocationJobService> _logger;

        public LocationJobService(ISqlRepository<Location, int> locationSqlRepository,
                                  IUnitOfWork unitOfWork,
                                  IMdpSystemService mdpSystemService,
                                  ILogger<LocationJobService> logger)
        {
            _locationSqlRepository = locationSqlRepository;
            _mdpSystemService = mdpSystemService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task MigrateMdpDataAsync()
        {
            try
            {
                var locations = await _locationSqlRepository.FindAsync(x => true);
                if (locations is null || !locations.Any())
                {
                    var locationServiceResponse = await _mdpSystemService.GetLocationsAsync();
                    if (!locationServiceResponse.IsError)
                    {
                        foreach (var location in locationServiceResponse.Locations)
                        {
                            var model = new Location
                            {
                                IsDeleted = location.EntityIsDeleted,
                                Name = location.EnglishName,
                                MdpId = location.EntityId,
                                DefaultCurrencyCode = location.DefaultCurrencyCode,
                                LeaderPMID = location.LeaderPMID,
                                Code = location.Code,
                                TimezoneName = location.TimezoneName,
                                CountryId = location.CountryId,
                            };
                            await _locationSqlRepository.AddAsync(model);
                        }
                        await _unitOfWork.SaveAsync();
                    }
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "couldn't migrate locations from MDP service");
            }            
        }

        public async Task SynchronizeMdpDataAsync()
        {
            try
            {
                var locationServiceResponse = await _mdpSystemService.GetLocationsAsync();
                var locations = await _locationSqlRepository.FindAsync(x => true);

                if (locations is null || !locations.Any())
                {
                    await MigrateMdpDataAsync();
                    return;
                }

                if (locationServiceResponse.IsError)
                {
                    throw new Exception(locationServiceResponse.ErrorMessage);
                }

                await SynchronizeData(locationServiceResponse, locations);

                await _unitOfWork.SaveAsync();
            }
            catch(Exception exception)
            {
                _logger.LogError(exception, "Exception occurred during synchronization of locations from MDP Service");
            }            
        }

        private async Task SynchronizeData(LocationsResponse locationServiceResponse, IEnumerable<Location> locations)
        {
            foreach (var mdpLocation in locationServiceResponse.Locations)
            {
                var location = locations.FirstOrDefault(x => x.MdpId == mdpLocation.EntityId);
                if (location == null)
                {
                    var newLocation = new Location
                    {
                        Name = mdpLocation.EnglishName,
                        IsDeleted = mdpLocation.EntityIsDeleted,
                        DefaultCurrencyCode = mdpLocation.DefaultCurrencyCode,
                        Code = mdpLocation.Code,
                        LeaderPMID = mdpLocation.LeaderPMID,
                        CountryId = mdpLocation.CountryId,
                        CountryCode = mdpLocation.CountryCode
                    };

                    await _locationSqlRepository.AddAsync(newLocation);
                }
                else
                {
                    if (location.IsDeleted != mdpLocation.EntityIsDeleted)
                    {
                        location.IsDeleted = mdpLocation.EntityIsDeleted;
                    }
                    await _locationSqlRepository.UpdateAsync(location);
                }
            }
        }
    }
}
