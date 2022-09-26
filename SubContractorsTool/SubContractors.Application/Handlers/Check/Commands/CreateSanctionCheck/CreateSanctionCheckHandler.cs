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

namespace SubContractors.Application.Handlers.Check.Commands.CreateSanctionCheck
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class CreateSanctionCheckHandler : IRequestHandler<CreateSanctionCheck, Result<int>>
    {
        private readonly ISqlRepository<SanctionCheck, int> _sqlRepository;
        private readonly ISqlRepository<SubContractor, int> _subContractorSqlRepository;
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _staffSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSanctionCheckHandler(ISqlRepository<SanctionCheck, int> sqlRepository, IUnitOfWork unitOfWork,
            ISqlRepository<SubContractor, int> subContractorSqlRepository,
            ISqlRepository<Domain.SubContractor.Staff.Staff, int> staffSqlRepository)
        {
            _sqlRepository = sqlRepository;
            _unitOfWork = unitOfWork;
            _subContractorSqlRepository = subContractorSqlRepository;
            _staffSqlRepository = staffSqlRepository;
        }

        public async Task<Result<int>> Handle(CreateSanctionCheck request, CancellationToken cancellationToken)
        {
            var check = new SanctionCheck();
            check.Create(request.Comment, request.Date.Value,
                (CheckStatus)request.CheckStatusId);

            var approver =
                await _staffSqlRepository.GetAsync(request.ApproverId.Value, Array.Empty<string>());
            if (approver == null)
            {
                return Result.NotFound<int>(
                    $"Approver entity wasn't found in database with provided identifier {request.ApproverId.Value}");
            }

            check.AssignApprover(approver);

            if ((ParentType)request.ParentType == ParentType.SubContractor)
            {
                var subContractor =
                    await _subContractorSqlRepository.GetAsync(x => x.Id == request.ParentId, Array.Empty<string>() );
                if (subContractor == null)
                {
                    return Result.NotFound<int>(
                        $"SubContractor wasn't found in database with provided identifier {request.ParentId}");
                }

                check.AssignToSubContractor(subContractor);
            }
            else
            {
                var staff = await _staffSqlRepository.GetAsync(x => x.Id == request.ParentId, Array.Empty<string>() );
                if (staff == null)
                {
                    return Result.NotFound<int>(
                        $"Staff wasn't found in database with provided identifier {request.ParentId}");
                }

                check.AssignToStaff(staff);
            }

            await _sqlRepository.AddAsync(check);

            await _unitOfWork.SaveAsync();

            return await Task.FromResult(Result.Success(ResultType.Created, data: check.Id));
        }
    }
}
