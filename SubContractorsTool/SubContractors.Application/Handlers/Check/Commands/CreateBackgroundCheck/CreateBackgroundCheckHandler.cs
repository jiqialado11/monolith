using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Check;

namespace SubContractors.Application.Handlers.Check.Commands.CreateBackgroundCheck
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class CreateBackgroundCheckHandler : IRequestHandler<CreateBackgroundCheck, Result<int>>
    {
        private readonly ISqlRepository<BackgroundCheck, int> _sqlRepository;
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _staffSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBackgroundCheckHandler(ISqlRepository<BackgroundCheck, int> sqlRepository, IUnitOfWork unitOfWork,
            ISqlRepository<Domain.SubContractor.Staff.Staff, int> staffSqlRepository)
        {
            _sqlRepository = sqlRepository;
            _unitOfWork = unitOfWork;
            _staffSqlRepository = staffSqlRepository;
        }

        public async Task<Result<int>> Handle(CreateBackgroundCheck request, CancellationToken cancellationToken)
        {
            var staff = await _staffSqlRepository.GetAsync(x => x.Id == request.StaffId, Array.Empty<string>() );
            if (staff == null)
            {
                return Result.NotFound<int>($"Staff wasn't found in database with provided identifier {request.StaffId}");
            }

            var check = new BackgroundCheck();
            check.Create(request.Link, request.Date.Value, (CheckStatus)request.CheckStatusId,
                staff);

            var approver =
                await _staffSqlRepository.GetAsync(request.ApproverId.Value, Array.Empty<string>());
            if (approver == null)
            {
                return Result.NotFound<int>(
                    $"Approver entity wasn't found in database with provided identifier {request.ApproverId.Value}");
            }

            check.AssignApprover(approver);

            await _sqlRepository.AddAsync(check);

            await _unitOfWork.SaveAsync();

            return await Task.FromResult(Result.Success(ResultType.Created, data: check.Id));
        }
    }
}
