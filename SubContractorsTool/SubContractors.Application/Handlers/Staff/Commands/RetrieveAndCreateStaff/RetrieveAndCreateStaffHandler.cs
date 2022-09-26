using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Common;
using SubContractors.Domain.SubContractor;
using SubContractors.Infrastructure.ExternalServices.PmCoreSystem;

namespace SubContractors.Application.Handlers.Staff.Commands.RetrieveAndCreateStaff
{
    [RequestLogging]
    [RequestValidation]
    [RequestInvalidateCache]
    public class RetrieveAndCreateStaffHandler : IRequestHandler<RetrieveAndCreateStaff, Result<int>>
    {
        private readonly IPmCoreSystemService _pmCoreSystemService;
        private readonly ISqlRepository<Location, int> _locationSqlRepository;
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _staffSqlRepository;
        private readonly ISqlRepository<SubContractor, int> _subContractorSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RetrieveAndCreateStaffHandler(IPmCoreSystemService pmCoreSystemService,
            ISqlRepository<Location, int> locationSqlRepository,
            ISqlRepository<Domain.SubContractor.Staff.Staff, int> staffSqlRepository, 
            ISqlRepository<SubContractor, int> subContractorSqlRepository,
            IUnitOfWork unitOfWork)
        {
            _pmCoreSystemService = pmCoreSystemService;
            _locationSqlRepository = locationSqlRepository;
            _staffSqlRepository = staffSqlRepository;
            _subContractorSqlRepository = subContractorSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(RetrieveAndCreateStaff request, CancellationToken cancellationToken)
        {
            var response = await _pmCoreSystemService.GetStaffDetailsAsync(request.PmId);
            var staff = new Domain.SubContractor.Staff.Staff();
            if (!response.IsError)
            {
                if (response.Data != null)
                {

                    var subContractor = await _subContractorSqlRepository.GetAsync(x => x.Id == request.SubContractorId);
                    if (subContractor == null)
                    {
                        return Result.NotFound<int>(
                            $"SubContractor wasn't found in database with provided identifier {request.SubContractorId}");
                    }

                    staff.Create(request.PmId, response.Data.Staff.FirstName, response.Data.Staff.LastName,
                        response.Data.Contacts.MainEmail, response.Data.Contacts.Skype, response.Data.Staff.Job,
                        response.Data.Staff.StaffFirstDate, response.Data.Staff.StaffLastDate,
                        string.Empty, response.Data.Phones!= null && response.Data.Phones.Any()? response.Data.Phones.FirstOrDefault().PhoneNumber : string.Empty,
                        response.Data.Staff.NdaSigned, response.Data.Staff.Department, response.Data.Staff.DomainLogin, response.Data.Staff.RealLocation, null);
                    
                    staff.AssignToSubContractor(subContractor);

                    await _staffSqlRepository.AddAsync(staff);
                    await _unitOfWork.SaveAsync();
                }
                else
                {
                    return Result.NotFound<int>($"Couldn't retrieve staff from PM System with provided identifier {request.PmId}");
                }
            }
            else
            {
                return Result.Fail<int>(ResultType.InternalServerError, $"Exception during request execution, please find more details in application logs");
            }

            return Result.Ok(value: staff.Id);
        }
    }
}
