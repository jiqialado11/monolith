using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractor
{
    [RequestLogging]
    [RequestValidation]
    public class GetSubContractorQueryHandler : IRequestHandler<GetSubContractorQuery, Result<GetSubContractorDto>>
    {
        private readonly ISqlRepository<SubContractor, int> _subContractorSqlRepository;
        private readonly IMapper _mapper;

        public GetSubContractorQueryHandler(ISqlRepository<SubContractor, int> subContractorSqlRepository, IMapper mapper)
        {
            _subContractorSqlRepository = subContractorSqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetSubContractorDto>> Handle(GetSubContractorQuery request, CancellationToken cancellationToken)
        {
            var subContractor = await _subContractorSqlRepository.GetAsync(request.Id.Value, new[]
            {
                nameof(SubContractor.Markets),
                nameof(SubContractor.Offices),
                nameof(SubContractor.Location),
                nameof(SubContractor.AccountManager)
            });

            if (subContractor == null)
            {
                return Result.NotFound<GetSubContractorDto>($"Couldn't find entity with provided identifier {request.Id.Value}");
            }

            var result = _mapper.Map<GetSubContractorDto>(subContractor);

            return Result.Ok(value: result);
        }
    }
}