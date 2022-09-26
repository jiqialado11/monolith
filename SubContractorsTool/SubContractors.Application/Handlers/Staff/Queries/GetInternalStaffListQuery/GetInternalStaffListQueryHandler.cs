using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor.Staff;

namespace SubContractors.Application.Handlers.Staff.Queries.GetInternalStaffListQuery
{
    [RequestLogging]
    [RequestValidation]
    [RequestCashing]
    public class GetInternalStaffListQueryHandler : IRequestHandler<GetInternalStaffListQuery, Result<IList<GetInternalStaffListDto>>>
    {
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _sqlRepository;
        private readonly IMapper _mapper;

        public GetInternalStaffListQueryHandler(ISqlRepository<Domain.SubContractor.Staff.Staff, int> sqlRepository, IMapper mapper)
        {
            _sqlRepository = sqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetInternalStaffListDto>>> Handle(GetInternalStaffListQuery request, CancellationToken cancellationToken)
        {
            var staffs = await _sqlRepository.FindAsync(x => x.Status == StaffStatus.Active);

            var enumerable = staffs.ToList();
            if (!enumerable.Any())
            {
                return Result.NotFound<IList<GetInternalStaffListDto>>("Couldn't find entities");
            }
            var result = enumerable.Select(s => _mapper.Map<GetInternalStaffListDto>(s)).ToList() as IList<GetInternalStaffListDto>;

            return Result.Ok(value: result);
        }
    }
}
