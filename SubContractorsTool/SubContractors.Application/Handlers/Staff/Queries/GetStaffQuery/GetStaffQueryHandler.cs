using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;

namespace SubContractors.Application.Handlers.Staff.Queries.GetStaffQuery
{
    [RequestLogging]
    [RequestValidation]
    public class GetStaffQueryHandler : IRequestHandler<GetStaffQuery, Result<GetStaffDto>>
    {
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _staffSqlRepository;
        private readonly IMapper _mapper;

        public GetStaffQueryHandler(ISqlRepository<Domain.SubContractor.Staff.Staff, int> staffSqlRepository, IMapper mapper)
        {
            _staffSqlRepository = staffSqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetStaffDto>> Handle(GetStaffQuery request, CancellationToken cancellationToken)
        {
            var staff = await _staffSqlRepository.GetAsync(request.Id.Value, new[]
            {
                nameof(Domain.SubContractor.Staff.Staff.BudgetOffice),
                nameof(Domain.SubContractor.Staff.Staff.SubContractors),
                nameof(Domain.SubContractor.Staff.Staff.Rates),
                nameof(Domain.SubContractor.Staff.Staff.Projects),
            });

            if (staff == null)
            {
                return Result.NotFound<GetStaffDto>($"Couldn't find entity with provided identifier {request.Id.Value}");
            }

            var result = _mapper.Map<GetStaffDto>(staff);

            return Result.Ok(value: result);
        }
    }
}
