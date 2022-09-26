using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Agreement;

namespace SubContractors.Application.Handlers.Agreement.Commands.UpdateRate
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class UpdateRateHandler : IRequestHandler<UpdateRate, Result<Unit>>
    {
        private readonly ISqlRepository<Rate, int> _rateSqlRepository;
        private readonly ISqlRepository<RateUnit, int> _rateUnitsSqlRepository;
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _staffsSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRateHandler(
            ISqlRepository<RateUnit, int> rateUnitsSqlRepository, 
            ISqlRepository<Domain.SubContractor.Staff.Staff, int> staffsSqlRepository, 
            IUnitOfWork unitOfWork, 
            ISqlRepository<Rate, int> rateSqlRepository)
        {
            _rateUnitsSqlRepository = rateUnitsSqlRepository;
            _staffsSqlRepository = staffsSqlRepository;
            _unitOfWork = unitOfWork;
            _rateSqlRepository = rateSqlRepository;
        }

        public async Task<Result<Unit>> Handle(UpdateRate request, CancellationToken cancellationToken)
        {
            Domain.SubContractor.Staff.Staff staff = null;

            var rate = await _rateSqlRepository.GetAsync(request.Id.Value, new string[] { nameof(Rate.Staff), nameof(Rate.Unit)});
            if (rate == null)
            {
                return Result.NotFound($"Rate wasn't found in database with provided identifier {request.Id.Value}");
            }

            if (request.StaffId != null || request.StaffId > 0)
            {
                staff = await _staffsSqlRepository.GetAsync(request.StaffId.Value, Array.Empty<string>());
                if (staff == null)
                {
                    return Result.NotFound($"staff wasn't found in database with provided identifier {request.StaffId}");
                }
            }            

            var rateUnit = await _rateUnitsSqlRepository.GetAsync(request.RateUnitId.Value, Array.Empty<string>() );
            if (rateUnit == null)
            {
                return Result.NotFound($"rate unit wasn't found in database with provided identifier {request.RateUnitId}");
            }

            rate.Update(request.Name,
                request.Rate.Value,
                request.FromDate.Value,
                request.ToDate.Value,
                request.Description);

            rate.AssignRateUnit(rateUnit);
            rate.AssignStaff(staff);

            await _rateSqlRepository.UpdateAsync(rate);

            await _unitOfWork.SaveAsync();

            return await Task.FromResult(Result.Accepted());
        }
    }
}
