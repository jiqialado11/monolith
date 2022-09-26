using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetOfficesQuery
{
    [RequestLogging]
    [RequestValidation]
    [RequestCashing]
    public class GetOfficesQueryHandler : IRequestHandler<GetOfficesQuery, Result<IList<GetOfficesDto>>>
    {
        private readonly ISqlRepository<Office, int> _sqlRepository;
        private readonly IMapper _mapper;

        public GetOfficesQueryHandler(ISqlRepository<Office, int> sqlRepository, IMapper mapper)
        {
            _sqlRepository = sqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetOfficesDto>>> Handle(GetOfficesQuery request, CancellationToken cancellationToken)
        {
            if (request.OfficeTypeId != null && !Enum.IsDefined(typeof(SubContractorStatus), request.OfficeTypeId))
            {
                return Result.NotFound<IList<GetOfficesDto>>($"Office type wasn't found with provided identifier {request.OfficeTypeId}");
            }

            var list = await _sqlRepository.FindAsync(x => x.OfficeType == (OfficeType) request.OfficeTypeId);

            var offices = list.ToList();
            if (!offices.Any())
            {
                return Result.NotFound<IList<GetOfficesDto>>("Couldn't find entities with provided parameters");
            }

            IList<GetOfficesDto> result = offices.Select(x => _mapper.Map<GetOfficesDto>(x))
                                                 .ToList();

            return Result.Ok(value: result);
        }
    }
}