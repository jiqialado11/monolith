using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Check;

namespace SubContractors.Application.Handlers.Check.Commands.UpdateBackgroundCheck
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class UpdateBackgroundCheckHandler : IRequestHandler<UpdateBackgroundCheck, Result<Unit>>
    {
        private readonly ISqlRepository<BackgroundCheck, int> _sqlRepository;
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _staffSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBackgroundCheckHandler(ISqlRepository<BackgroundCheck, int> sqlRepository, IUnitOfWork unitOfWork,
            ISqlRepository<Domain.SubContractor.Staff.Staff, int> staffSqlRepository)
        {
            _sqlRepository = sqlRepository;
            _unitOfWork = unitOfWork;
            _staffSqlRepository = staffSqlRepository;
        }

        public async Task<Result<Unit>> Handle(UpdateBackgroundCheck request, CancellationToken cancellationToken)
        {
            var staff = await _staffSqlRepository.GetAsync(x => x.Id == request.StaffId, Array.Empty<string>() );
            if (staff == null)
            {
                return Result.NotFound($"Staff wasn't found in database with provided identifier {request.StaffId}");
            }

            var check = await _sqlRepository.GetAsync(x => x.Id == request.CheckId, Array.Empty<string>() );
            if (check == null)
            {
                return Result.NotFound(
                    $"Background check wasn't found in database with provided identifier {request.CheckId}");
            }

            check.Update(request.Link, request.Date.Value, (CheckStatus)request.CheckStatusId,
                staff);

            if (check.Approver == null || check.Approver.Id != request.ApproverId)
            {
                var approver =
                    await _staffSqlRepository.GetAsync(request.ApproverId.Value, Array.Empty<string>());
                if (approver == null)
                {
                    return Result.NotFound<Unit>(
                        $"Approver entity wasn't found in database with provided identifier {request.ApproverId.Value}");
                }

                check.AssignApprover(approver);
            }


            await _sqlRepository.UpdateAsync(check);

            await _unitOfWork.SaveAsync();

            return await Task.FromResult(Result.Accepted());
        }
    }
}
