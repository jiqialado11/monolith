using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Check;

namespace SubContractors.Application.Handlers.Check.Queries.GetBackgroundChecksQuery
{
    [RequestLogging]
    [RequestValidation]
    public class
        GetBackgroundChecksQueryHandler : IRequestHandler<GetBackgroundChecksQuery,
            Result<IList<GetBackgroundChecksDto>>>
    {
        private readonly ISqlRepository<Domain.Check.BackgroundCheck, int> _sqlRepository;
        private readonly IMapper _mapper;

        public GetBackgroundChecksQueryHandler(ISqlRepository<Domain.Check.BackgroundCheck, int> sqlRepository,
            IMapper mapper)
        {
            _sqlRepository = sqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetBackgroundChecksDto>>> Handle(GetBackgroundChecksQuery request,
            CancellationToken cancellationToken)
        {
            var list = await _sqlRepository.FindAsync(x => x.Staff.Id == request.StaffId, new string []{nameof(BackgroundCheck.Approver)} );

            var checks = list.ToList();
            if (!checks.Any())
            {
                return Result.NotFound<IList<GetBackgroundChecksDto>>(
                    $"Staff with identifier {request.StaffId} doesn't have Background Checks");
            }

            IList<GetBackgroundChecksDto> result = checks.Select(s => _mapper.Map<GetBackgroundChecksDto>(s)).ToList();

            return Result.Ok(value: result);
        }
    }
}
