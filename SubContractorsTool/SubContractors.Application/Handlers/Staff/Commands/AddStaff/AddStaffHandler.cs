using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Handlers.Staff.Commands.AddStaff
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    [RequestInvalidateCache]
    public class AddStaffHandler : IRequestHandler<AddStaff, Result<Unit>>
    {
        private readonly ISqlRepository<SubContractor, int> _subContractorSqlRepository;
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _staffSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddStaffHandler(ISqlRepository<SubContractor, int> subContractorSqlRepository,
            ISqlRepository<Domain.SubContractor.Staff.Staff, int> staffSqlRepository,
            IUnitOfWork unitOfWork)
        {
            _subContractorSqlRepository = subContractorSqlRepository;
            _staffSqlRepository = staffSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(AddStaff request, CancellationToken cancellationToken)
        {
            var subContractor = await _subContractorSqlRepository.GetAsync(x => x.Id == request.SubContractorId, new[]{nameof(SubContractor.Staffs)});
            if (subContractor == null)
            {
                return Result.NotFound<Unit>(
                    $"SubContractor wasn't found in database with provided identifier {request.SubContractorId}");
            }

            var staff = await _staffSqlRepository.GetAsync(x => x.Id == request.StaffId);
            if (staff == null)
            {
                return Result.NotFound<Unit>(
                    $"Staff wasn't found in database with provided identifier {request.StaffId}");
            }
            
            if (!subContractor.AssignStaff(staff))
            {
                return Result.Fail<Unit>(ResultType.BadRequest,
                    $"Staff with Identifier - {request.StaffId} already exists in SubContractor staff list");
            }

            await _subContractorSqlRepository.UpdateAsync(subContractor);
            await _unitOfWork.SaveAsync();
            return Result.Success(ResultType.Accepted);
        }
    }
}
