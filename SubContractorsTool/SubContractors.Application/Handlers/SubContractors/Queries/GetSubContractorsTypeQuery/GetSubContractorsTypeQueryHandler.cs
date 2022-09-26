using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractorsTypeQuery
{
    [RequestLogging]
    public class GetSubContractorsTypeQueryHandler : IRequestHandler<GetSubContractorsTypeQuery, Result<IList<GetSubContractorTypesDto>>>
    {
        private readonly IMapper _mapper;

        public GetSubContractorsTypeQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<Result<IList<GetSubContractorTypesDto>>> Handle(GetSubContractorsTypeQuery request, CancellationToken cancellationToken)
        {
            var subContractorTypes = Enum.GetValues(typeof(SubContractorType))
                                         .Cast<SubContractorType>()
                                         .ToList();

            if (!subContractorTypes.Any())
            {
                return Task.FromResult(Result.NotFound<IList<GetSubContractorTypesDto>>("Couldn't find entities with provided parameters"));
            }

            IList<GetSubContractorTypesDto> result = subContractorTypes.Select(x => _mapper.Map<GetSubContractorTypesDto>(x))
                                                                       .ToList();

            return Task.FromResult(Result.Ok(value: result));
        }
    }
}