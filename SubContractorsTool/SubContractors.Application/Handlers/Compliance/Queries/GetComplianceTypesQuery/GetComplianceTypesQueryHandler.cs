using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Compliance;

namespace SubContractors.Application.Handlers.Compliance.Queries.GetComplianceTypesQuery
{
    [RequestLogging]
    public class GetComplianceTypesQueryHandler : IRequestHandler<GetComplianceTypesQuery, Result<IList<GetComplianceTypeDto>>>
    {
        private readonly IMapper _mapper;

        public GetComplianceTypesQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public  Task<Result<IList<GetComplianceTypeDto>>> Handle(GetComplianceTypesQuery request, CancellationToken cancellationToken)
        {
            var complianceTypes = Enum.GetValues(typeof(ComplianceType))
                .Cast<ComplianceType>()
                .ToList();

            if (!complianceTypes.Any())
            {
                return Task.FromResult(Result.NotFound<IList<GetComplianceTypeDto>>("Couldn't find entities with provided parameters"));
            }

            IList<GetComplianceTypeDto> result = complianceTypes.Select(x => _mapper.Map<GetComplianceTypeDto>(x))
                .ToList();

            return Task.FromResult(Result.Ok(value: result));
        }
    }
}
