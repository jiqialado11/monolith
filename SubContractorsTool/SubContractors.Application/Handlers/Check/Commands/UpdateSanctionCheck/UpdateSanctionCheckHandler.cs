using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Application.Handlers.Check.Queries.GetSanctionChecksQuery;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Check;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Handlers.Check.Commands.UpdateSanctionCheck
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class UpdateSanctionCheckHandler : IRequestHandler<UpdateSanctionCheck, Result<Unit>>
    {
        private readonly ISqlRepository<SanctionCheck, int> _sqlRepository;
        private readonly ISqlRepository<SubContractor, int> _subContractorSqlRepository;
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _staffSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSanctionCheckHandler(ISqlRepository<SanctionCheck, int> sqlRepository,
            ISqlRepository<SubContractor, int> subContractorSqlRepository, IUnitOfWork unitOfWork,
            ISqlRepository<Domain.SubContractor.Staff.Staff, int> staffSqlRepository)
        {
            _sqlRepository = sqlRepository;
            _subContractorSqlRepository = subContractorSqlRepository;
            _unitOfWork = unitOfWork;
            _staffSqlRepository = staffSqlRepository;
        }

        public async Task<Result<Unit>> Handle(UpdateSanctionCheck request, CancellationToken cancellationToken)
        {
            var check = await _sqlRepository.GetAsync(x => x.Id == request.CheckId, new string[]{nameof(SanctionCheck.Approver)});
            if (check == null)
            {
                return Result.NotFound(
                    $"Sanction check wasn't found in database with provided identifier {request.CheckId}");
            }

            check.Update(request.Comment, 
                request.Date.Value,
                (CheckStatus)request.CheckStatusId.Value);


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


            if ((ParentType)request.ParentType == ParentType.SubContractor)
            {
                var subContractor =
                    await _subContractorSqlRepository.GetAsync(x => x.Id == request.ParentId, Array.Empty<string>() );
                if (subContractor == null)
                {
                    return Result.NotFound(
                        $"SubContractor wasn't found in database with provided identifier {request.ParentId}");
                }

                check.AssignToSubContractor(subContractor);
            }
            else
            {
                var staff = await _staffSqlRepository.GetAsync(x => x.Id == request.ParentId, Array.Empty<string>() );
                if (staff == null)
                {
                    return Result.NotFound(
                        $"Staff wasn't found in database with provided identifier {request.ParentId}");
                }

                check.AssignToStaff(staff);
            }

            await _sqlRepository.UpdateAsync(check);

            await _unitOfWork.SaveAsync();

            return await Task.FromResult(Result.Accepted());
        }
    }
}
