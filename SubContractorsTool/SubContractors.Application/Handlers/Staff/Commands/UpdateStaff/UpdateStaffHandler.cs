using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Common;

namespace SubContractors.Application.Handlers.Staff.Commands.UpdateStaff
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    [RequestInvalidateCache]

    public class UpdateStaffHandler : IRequestHandler<UpdateStaff, Result<Unit>>
    {
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _staffSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStaffHandler(
            ISqlRepository<Domain.SubContractor.Staff.Staff, int> staffSqlRepository, 
            IUnitOfWork unitOfWork)
        {
            _staffSqlRepository = staffSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(UpdateStaff request, CancellationToken cancellationToken)
        {
            var staff = await _staffSqlRepository.GetAsync(x => x.Id == request.Id,
                new string[] { nameof(Domain.SubContractor.Staff.Staff.Location)});
            if (staff == null)
            {
                return Result.NotFound($"Staff wasn't found in database with provided identifier {request.Id}");
            }

            staff.Update(request.Email, request.Skype, 
                         request.StartDate.Value, request.EndDate.Value,
                         request.CellPhone, request.DepartmentName,
                         request.DomainLogin, null, null);


            await _staffSqlRepository.UpdateAsync(staff);
            await _unitOfWork.SaveAsync();
            return Result.Success(ResultType.Accepted);
        }
    }
}
