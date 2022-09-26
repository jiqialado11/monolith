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

namespace SubContractors.Application.Handlers.Check.Queries.GetSanctionChecksQuery
{
    [RequestLogging]
    [RequestValidation]
    public class
        GetSanctionChecksQueryHandler : IRequestHandler<GetSanctionChecksQuery, Result<IList<GetSanctionChecksDto>>>
    {
        private readonly ISqlRepository<Domain.Check.SanctionCheck, int> _sqlRepository;
        private readonly IMapper _mapper;

        public GetSanctionChecksQueryHandler(ISqlRepository<Domain.Check.SanctionCheck, int> sqlRepository,
            IMapper mapper)
        {
            _sqlRepository = sqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetSanctionChecksDto>>> Handle(GetSanctionChecksQuery request,
            CancellationToken cancellationToken)
        {
            IEnumerable<SanctionCheck> list;
            if ((ParentType)request.ParentType == ParentType.SubContractor)
            {
                list = await _sqlRepository.FindAsync(x => x.SubContractor.Id == request.ParentId,
                    new string[] { nameof(SanctionCheck.SubContractor), nameof(SanctionCheck.Approver) });
            }
            else
            {
                list = await _sqlRepository.FindAsync(x => x.Staff.Id == request.ParentId,
                    new string[] { nameof(SanctionCheck.Staff), nameof(SanctionCheck.Approver) });
            }

            var checks = list.ToList();
            if (!checks.Any())
            {
                var parentName = Enum.GetName((ParentType)request.ParentType);
                return Result.NotFound<IList<GetSanctionChecksDto>>($"{parentName} with identifier {request.ParentId} doesn't have Sanction Checks");
            }

            IList<GetSanctionChecksDto> result = checks.Select(s => _mapper.Map<GetSanctionChecksDto>(s)).ToList();

            return Result.Ok(value: result);
        }
    }
}
