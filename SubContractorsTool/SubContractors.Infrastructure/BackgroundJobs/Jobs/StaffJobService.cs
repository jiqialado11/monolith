using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.SubContractor.Staff;
using SubContractors.Infrastructure.BackgroundJobs.Interfaces;
using SubContractors.Infrastructure.ExternalServices.PmCoreSystem;

namespace SubContractors.Infrastructure.BackgroundJobs.Jobs
{
    public class StaffJobService : IStaffJobService
    {
        private readonly ISqlRepository<Staff, int> _sqlRepository;
        private readonly IPmCoreSystemService _pmCoreSystemService;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public StaffJobService(IPmCoreSystemService pmCoreSystemService,
                              ILogger<StaffJobService> logger,
                              IUnitOfWork unitOfWork,
                              ISqlRepository<Staff, int> sqlRepository)
        {
            _pmCoreSystemService = pmCoreSystemService;
            _sqlRepository = sqlRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task MigratePmDataAsync()
        {
            try
            {
                var staffCollection = await _sqlRepository.FindAsync(x => x.StartDate != null);
                if (!staffCollection.Any())
                {
                    var pmStaffResponse = await _pmCoreSystemService.GetStaffListPerLastDaysAsync(90);

                    foreach (var staff in pmStaffResponse.StaffList)
                    {
                        var newStaff = new Staff();

                        newStaff.Create(staff.StaffId.Value, staff.FirstName, staff.LastName, staff.Email,
                                        null, null, null, null, null, null, null, staff.DepartmentName, null,
                                        staff.Location, staff.StaffStartDate);

                        await _sqlRepository.AddAsync(newStaff);
                    }

                    await _unitOfWork.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task SynchronizePmDataAsync()
        {
            try
            {
                var filerDate = DateTime.Today.AddDays(-1);
                var existStaffCollection = await _sqlRepository.FindAsync(x => x.StartDate >= filerDate);
                var pmSfaffResponse = await _pmCoreSystemService.GetStaffListPerLastDaysAsync(2);
                var pmStaffCollection = pmSfaffResponse.StaffList
                                                       .Where(x => x.StaffStartDate >= filerDate)
                                                       .ToList();
                foreach (var pmStaff in pmStaffCollection)
                {
                    var staff = existStaffCollection.FirstOrDefault(x => x.PmId == pmStaff.StaffId);
                    if (staff != null)
                    {
                        staff.Update(pmStaff.Email, null, null, null, null, pmStaff.DepartmentName,
                                     null, pmStaff.StaffStartDate, pmStaff.Location);

                        await _sqlRepository.UpdateAsync(staff);
                    }
                    else
                    {
                        var newStaff = new Staff();
                        newStaff.Create(pmStaff.StaffId.Value, pmStaff.FirstName, pmStaff.LastName, pmStaff.Email,
                                        null, null, null, null, null, null, null, pmStaff.DepartmentName, null,
                                        pmStaff.Location, pmStaff.StaffStartDate);

                        await _sqlRepository.AddAsync(newStaff);
                    }
                }

                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
