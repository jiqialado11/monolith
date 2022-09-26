using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.Common;
using SubContractors.Domain.SubContractor;
using SubContractors.Infrastructure.BackgroundJobs.Interfaces;
using SubContractors.Infrastructure.ExternalServices.MDPSystem;
using SubContractors.Infrastructure.ExternalServices.MDPSystem.ResponseModels.VendorData;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractors.Infrastructure.BackgroundJobs.Jobs
{
    public class VendorJobService : IVendorJobService
    {
        private readonly ISubContractorSqlRepository _subContractorSqlRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMdpSystemService _mdpSystemService;
        private readonly ILogger<VendorJobService> _logger;

        public VendorJobService(
            ISubContractorSqlRepository subContractorSqlRepository, 
            IUnitOfWork unitOfWork, 
            IMdpSystemService mdpSystemService, 
            ILogger<VendorJobService> logger
            )
        {
            _subContractorSqlRepository = subContractorSqlRepository;
            _unitOfWork = unitOfWork;
            _mdpSystemService = mdpSystemService;
            _logger = logger;
        }

        public async Task MigrateMdpDataAsync()
        {
            try
            {
                var subContractorsExist = await _subContractorSqlRepository.ExistsAsync(x=> true);

                if (subContractorsExist)
                {
                    return;
                }
                var subContractorsServiceResponse = await _mdpSystemService.GetSubcontractors();
                    if (!subContractorsServiceResponse.IsError)
                    {
                        var models = new List<SubContractor>();
                        foreach (var subContractor in subContractorsServiceResponse.Data)
                        {
                            
                            var model = new SubContractor
                            {
                                IsDeleted = subContractor.IsDeleted,
                                IsArchived = subContractor.IsArchived,
                                Name = subContractor.EnglishName,
                                MdpId = subContractor.EntityId,
                                ExternalId = subContractor.externalId,
                                SubContractorStatus = SubContractorStatus.Tentative

                            };
                            models.Add(model);
                           
                        }
                        await _subContractorSqlRepository.AddRangeAsync(models);
                    await _unitOfWork.SaveAsync();
                    }
                
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "couldn't migrate SubContractors from MDP service");
            }
        }

        public async Task SynchronizeMdpDataAsync()
        {
            try
            {
                var subContractorsServiceResponse = await _mdpSystemService.GetSubcontractors();
                var subContractors = await _subContractorSqlRepository.FindAsync(x => x.IsDeleted == false && x.IsArchived == false);

                if (subContractors is null || !subContractors.Any())
                {
                    await MigrateMdpDataAsync();
                    return;
                }

                if (subContractorsServiceResponse.IsError)
                {
                    throw new Exception(subContractorsServiceResponse.ErrorMessage);
                }

                await SynchronizeData(subContractorsServiceResponse, subContractors);

                await _unitOfWork.SaveAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception occurred during synchronization of SubContractors from MDP Service");
            }
        }

        private async Task SynchronizeData(VendorsResponse subContractorsServiceResponse, IEnumerable<SubContractor> subContractors)
        {
            foreach (var mdpSubContractors in subContractorsServiceResponse.Data)
            {
                var subContractor = subContractors.FirstOrDefault(x => x.MdpId == mdpSubContractors.EntityId);
                if (subContractor == null)
                {
                    var newSubContractor = new SubContractor
                    {
                        IsDeleted = mdpSubContractors.IsDeleted,
                        IsArchived = mdpSubContractors.IsArchived,
                        Name = mdpSubContractors.EnglishName,
                        MdpId = mdpSubContractors.EntityId,
                        ExternalId = mdpSubContractors.externalId,
                        SubContractorStatus = SubContractorStatus.Tentative
                    };

                    await _subContractorSqlRepository.AddAsync(newSubContractor);
                }
                else
                {
                    if (subContractor.IsDeleted != mdpSubContractors.IsDeleted)
                    {
                        subContractor.IsDeleted = mdpSubContractors.IsDeleted;
                    }

                    if (subContractor.IsArchived != mdpSubContractors.IsArchived)
                    {
                        subContractor.IsArchived = mdpSubContractors.IsArchived;
                    }
                    await _subContractorSqlRepository.UpdateAsync(subContractor);
                }
            }
        }
    }
}
