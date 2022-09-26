using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.SubContractor;
using SubContractors.Infrastructure.BackgroundJobs.Interfaces;
using SubContractors.Infrastructure.ExternalServices.MDPSystem;

namespace SubContractors.Infrastructure.BackgroundJobs.Jobs
{
    public class LegalEntityJobService : ILegalEntityJobService
    {
        private readonly ISqlRepository<LegalEntity, int> _leSqlRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMdpSystemService _mdpSystemService;
        private readonly ILogger _logger;

        public LegalEntityJobService(ISqlRepository<LegalEntity, int> sqlRepository,
                                     IUnitOfWork unitOfWork,
                                     IMdpSystemService mdpSystemService,
                                     ILogger<LegalEntityJobService> logger)
        {
            _leSqlRepository = sqlRepository;
            _unitOfWork = unitOfWork;
            _mdpSystemService = mdpSystemService;
            _logger = logger;
        }

        public async Task MigrateMdpDataAsync()
        {
            try
            {
                var leCollection = await _leSqlRepository.FindAsync(x => true);
                if (leCollection is null || !leCollection.Any())
                {

                    var legalEntityData = await _mdpSystemService.GetLegalEntitiesAsync();
                    foreach (var leItem in legalEntityData.Data)
                    {
                         var leModel = new LegalEntity
                            {
                                EnglishName = leItem.EnglishName,
                                AddressInEnglish = leItem.AddressInEnglish,
                                AddressInLocalLanguage = leItem.AddressInLocalLanguage,
                                CityName = leItem.CityName,
                                CountryName = leItem.CountryName,
                                HeadPositionEnglishName = leItem.HeadPositionEnglishName,
                                HeadPositionLocalLanguageName = leItem.HeadPositionLocalLanguageName,
                                LegalRegistrationCode = leItem.LegalRegistrationCode,
                                TaxNumber = leItem.TaxNumber,
                                VersionId = leItem.VersionId,
                                IsActive = leItem.IsActive,
                                IsDeleted = leItem.EntityIsDeleted,
                                MdpId = leItem.EntityId
                            };

                            await _leSqlRepository.AddAsync(leModel);
                        
                    }
                    await _unitOfWork.SaveAsync();
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }

        }

        public async Task SynchronizeMdpDataAsync()
        {
            try
            {
                var legalEntitiesCollection = await _leSqlRepository.FindAsync(x => true);
                var mdpLegalEntities = await _mdpSystemService.GetLegalEntitiesAsync();
                foreach (var mdpItem in mdpLegalEntities.Data)
                {
                    var leModel = legalEntitiesCollection.FirstOrDefault(x => x.MdpId == mdpItem.EntityId);
                    if (leModel is null)
                    {
                        var model = new LegalEntity
                        {
                            EnglishName = mdpItem.EnglishName,
                            AddressInEnglish = mdpItem.AddressInEnglish,
                            AddressInLocalLanguage = mdpItem.AddressInLocalLanguage,
                            CityName = mdpItem.CityName,
                            CountryName = mdpItem.CountryName,
                            HeadPositionEnglishName = mdpItem.HeadPositionEnglishName,
                            HeadPositionLocalLanguageName = mdpItem.HeadPositionLocalLanguageName,
                            LegalRegistrationCode = mdpItem.LegalRegistrationCode,
                            TaxNumber = mdpItem.TaxNumber,
                            VersionId = mdpItem.VersionId,
                            IsActive = mdpItem.IsActive,
                            IsDeleted = mdpItem.EntityIsDeleted,
                            MdpId = mdpItem.EntityId
                        };

                        await _leSqlRepository.AddAsync(model);
                    }
                    else
                    {
                        if (leModel.IsActive != mdpItem.IsActive)
                        {
                            leModel.IsActive = mdpItem.IsActive;
                        }

                        if (leModel.IsDeleted != mdpItem.EntityIsDeleted)
                        {
                            leModel.IsDeleted = mdpItem.EntityIsDeleted;
                        }

                        await _leSqlRepository.UpdateAsync(leModel);

                    }
                }
                await _unitOfWork.SaveAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }
        }
    }
}

