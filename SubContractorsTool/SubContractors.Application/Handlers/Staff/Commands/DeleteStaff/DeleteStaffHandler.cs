using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractors.Application.Handlers.Staff.Commands.DeleteStaff
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    [RequestInvalidateCache]
    public class DeleteStaffHandler : IRequestHandler<DeleteStaff, Result<Unit>>
    {
        private readonly ISubContractorSqlRepository _subContractorSqlRepository;
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _staffSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStaffHandler(ISubContractorSqlRepository subContractorSqlRepository, ISqlRepository<Domain.SubContractor.Staff.Staff, int> staffSqlRepository, IUnitOfWork unitOfWork)
        {
            _subContractorSqlRepository = subContractorSqlRepository;
            _staffSqlRepository = staffSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteStaff request, CancellationToken cancellationToken)
        {
            var subContractor = await _subContractorSqlRepository.GetWithStaffRelatedEntitiesAsync(request.SubContractorId.Value);
            if (subContractor == null)
            {
                return Result.NotFound<Unit>($"SubContractor wasn't found in database with provided identifier {request.SubContractorId}");
            }

            var staff = await _staffSqlRepository.GetAsync(request.StaffId.Value, new string[] { });
            if (staff == null)
            {
                return Result.NotFound<Unit>($"Staff wasn't found in database with provided identifier {request.StaffId}");
            }

            foreach (var addendum in subContractor.Agreements.SelectMany(agreement => agreement.Addenda.Where(addendum => addendum.Staffs.Contains(staff))))
            {
                return Result.Fail<Unit>(ResultType.BadRequest, $"Couldn't remove staff with identifier {request.StaffId} from subcontractor because  it's linked with addendum {addendum.Id}");
            }

            var successfullyRemoved = subContractor.RemoveStaff(staff);

            if (!successfullyRemoved)
            {
                return Result.NotFound<Unit>($"SubContractor with identifier {request.SubContractorId} doesn't have staff with identifier {request.StaffId}");
            }

            await _subContractorSqlRepository.UpdateAsync(subContractor);
            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}
