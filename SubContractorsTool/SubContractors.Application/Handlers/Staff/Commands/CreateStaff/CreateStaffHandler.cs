using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Common;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Handlers.Staff.Commands.CreateStaff
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    [RequestInvalidateCache]
    public class CreateStaffHandler : IRequestHandler<CreateStaff, Result<int>>
    {
        private readonly ISqlRepository<SubContractor, int> _subContractorSqlRepository;
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _staffSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateStaffHandler(ISqlRepository<SubContractor, int> subContractorSqlRepository,
            ISqlRepository<Domain.SubContractor.Staff.Staff, int> staffSqlRepository,
            IUnitOfWork unitOfWork)
        {
            _subContractorSqlRepository = subContractorSqlRepository;
            _staffSqlRepository = staffSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateStaff request, CancellationToken cancellationToken)
        {
            var staffAlreadyExists = await _staffSqlRepository.ExistsAsync(x => x.PmId == request.PmId);
            if (staffAlreadyExists)
            {
                return Result.Fail<int>(ResultType.BadRequest,
                    $"Staff with PM Identifier {request.PmId} already exists in database");
            }

            var subContractor = await _subContractorSqlRepository.GetAsync(x => x.Id == request.SubContractorId);
            if (subContractor == null)
            {
                return Result.NotFound<int>(
                    $"SubContractor wasn't found in database with provided identifier {request.SubContractorId}");
            }
            var staff = new Domain.SubContractor.Staff.Staff();

            staff.Create(request.PmId.Value, request.FirstName, request.LastName,
                request.Email, request.Skype, request.Position,
                request.StartDate, request.EndDate, 
                request.Qualifications, request.CellPhone, request.IsNdaSigned,
                request.DepartmentName, request.DomainLogin, request.RealLocation, null);
            staff.AssignToSubContractor(subContractor);
            await _staffSqlRepository.AddAsync(staff);
            await _unitOfWork.SaveAsync();
            return Result.Success(ResultType.Created, data: staff.Id);
        }
    }
}