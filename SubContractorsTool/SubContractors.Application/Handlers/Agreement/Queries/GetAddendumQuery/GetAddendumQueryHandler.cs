using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractors.Application.Handlers.Agreement.Queries.GetAddendumQuery
{
    [RequestLogging]
    [RequestValidation]
    public class GetAddendumQueryHandler : IRequestHandler<GetAddendumQuery, Result<GetAddendumDto>>
    {
        private readonly IAddendaSqlRepository _addendaSqlRepository;
        private readonly IMapper _mapper;

        public GetAddendumQueryHandler(IAddendaSqlRepository addendaSqlRepository, IMapper mapper)
        {
            _addendaSqlRepository = addendaSqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetAddendumDto>> Handle(GetAddendumQuery request, CancellationToken cancellationToken)
        {
            var addendum = await _addendaSqlRepository.GetAsync(request.Id.Value);

            if (addendum == null)
            {
                return Result.NotFound<GetAddendumDto>($"Couldn't find addendum with provided identifier {request.Id.Value}");
            }

            var result = _mapper.Map<GetAddendumDto>(addendum);

            return Result.Ok(value: result);
        }
    }
}