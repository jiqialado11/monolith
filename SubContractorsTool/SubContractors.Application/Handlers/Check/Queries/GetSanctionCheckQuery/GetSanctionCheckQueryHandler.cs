    using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Check;

namespace SubContractors.Application.Handlers.Check.Queries.GetSanctionCheckQuery
{
    [RequestLogging]
    [RequestValidation]
    public class GetSanctionCheckQueryHandler : IRequestHandler<GetSanctionCheckQuery,
        Result<GetSanctionCheckDto>>
    {

        private readonly ISqlRepository<SanctionCheck, int> _sqlRepository;
        private readonly IMapper _mapper;

        public GetSanctionCheckQueryHandler(ISqlRepository<SanctionCheck, int> sqlRepository, IMapper mapper)
        {
            _sqlRepository = sqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetSanctionCheckDto>> Handle(GetSanctionCheckQuery request, CancellationToken cancellationToken)
        {
            var check = await _sqlRepository.GetAsync(request.Id.Value, new string[] { nameof(SanctionCheck.Approver) });

            if (check == null)
            {
                return Result.NotFound<GetSanctionCheckDto>($"Couldn't find sanction check with provided identifier {request.Id.Value}");
            }

            var result = _mapper.Map<GetSanctionCheckDto>(check);

            return Result.Ok(value: result);
        }
    }
}
