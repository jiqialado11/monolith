using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Agreement;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractors.Application.Handlers.Agreement.Commands.AddRate
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class AddRateHandler : IRequestHandler<AddRate, Result<int>>
    {
        private readonly IAddendaSqlRepository _sqlRepository;
        private readonly ISqlRepository<RateUnit, int> _rateUnitsSqlRepository;
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _staffsSqlRepository;
        private readonly ISqlRepository<Rate, int> _rateSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddRateHandler(
            IAddendaSqlRepository sqlRepository, 
            ISqlRepository<RateUnit, int> rateUnitsSqlRepository, 
            ISqlRepository<Domain.SubContractor.Staff.Staff, int> staffsSqlRepository, 
            IUnitOfWork unitOfWork, ISqlRepository<Rate, int> rateSqlRepository)
        {
            _sqlRepository = sqlRepository;
            _rateUnitsSqlRepository = rateUnitsSqlRepository;
            _staffsSqlRepository = staffsSqlRepository;
            _unitOfWork = unitOfWork;
            _rateSqlRepository = rateSqlRepository;
        }

        public async Task<Result<int>> Handle(AddRate request, CancellationToken cancellationToken)
        {
            Domain.SubContractor.Staff.Staff staff = null;
            var addendum = await _sqlRepository.GetAsync(request.AddendumId.Value, Array.Empty<string>() );
            if (addendum == null)
            {
                return Result.NotFound<int>($"Addendum wasn't found in database with provided identifier {request.AddendumId.Value}");
            }

            if (request.StaffId.HasValue)
            {
                staff = await _staffsSqlRepository.GetAsync(request.StaffId.Value, Array.Empty<string>());
                if (staff == null)
                {
                    return Result.NotFound<int>($"staff wasn't found in database with provided identifier {request.StaffId}");
                }
            }
            

            var rateUnit = await _rateUnitsSqlRepository.GetAsync(request.RateUnitId.Value, Array.Empty<string>());
            if (rateUnit == null)
            {
                return Result.NotFound<int>($"rate unit wasn't found in database with provided identifier {request.RateUnitId}");
            }

            var rate = new Rate
            {
                Name = request.Name,
                Description = request.Description,
                Addendum = addendum,
                FromDate = request.FromDate.Value,
                RateValue = request.Rate.Value,
                ToDate = request.ToDate.Value,
                Unit = rateUnit
            };

            if (staff != null)
            {
                rate.AssignStaff(staff);
            }
            addendum.AddRate(rate);

            await _rateSqlRepository.AddAsync(rate);
            await _unitOfWork.SaveAsync();

            return await Task.FromResult(Result.Success(ResultType.Created, data: rate.Id));
        }
    }
}
