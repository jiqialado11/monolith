using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractors.Application.Handlers.Agreement.Queries.GetAgreementQuery
{
    [RequestLogging]
    [RequestValidation]
    public class GetAgreementQueryHandler : IRequestHandler<GetAgreementQuery, Result<GetAgreementDto>>
    {
        private readonly IAgreementSqlRepository _agreementSqlRepository;
        private readonly IMapper _mapper;

        public GetAgreementQueryHandler(IAgreementSqlRepository agreementSqlRepository, IMapper mapper)
        {
            _agreementSqlRepository = agreementSqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetAgreementDto>> Handle(GetAgreementQuery request, CancellationToken cancellationToken)
        {
            var agreement = await _agreementSqlRepository.GetAsync(request.Id.Value);

            if (agreement == null)
            {
                return Result.NotFound<GetAgreementDto>($"Couldn't find agreement with provided identifier {request.Id.Value}");
            }

            var result = _mapper.Map<GetAgreementDto>(agreement);

            return Result.Ok(value: result);
        }
    }
}