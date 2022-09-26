using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractorsQuery
{
    [RequestLogging]
    [RequestCashing]
    public class GetSubContractorsQueryHandler : IRequestHandler<GetSubContractorsQuery, Result<IList<GetSubContractorsDto>>>
    {
        private readonly ISqlRepository<SubContractor, int> _sqlRepository;

        public GetSubContractorsQueryHandler(ISqlRepository<SubContractor, int> sqlRepository)
        {
            _sqlRepository = sqlRepository;
        }

        public async Task<Result<IList<GetSubContractorsDto>>> Handle(GetSubContractorsQuery request, CancellationToken cancellationToken)
        {
            var list = await _sqlRepository.FindAsync(x => x.SubContractorStatus == SubContractorStatus.Active);

            var subContractors = list.ToList();
            if (!subContractors.Any())
            {
                return Result.NotFound<IList<GetSubContractorsDto>>("Couldn't find entities with provided parameters");
            }

            IList<GetSubContractorsDto> result = subContractors.Select(s => new GetSubContractorsDto
                                                                {
                                                                    Id = s.Id,
                                                                    Name = s.Name
                                                                })
                                                               .ToList();
            return Result.Ok(value: result);
        }
    }
}