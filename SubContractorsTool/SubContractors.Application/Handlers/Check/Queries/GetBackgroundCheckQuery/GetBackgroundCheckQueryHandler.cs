using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Check;

namespace SubContractors.Application.Handlers.Check.Queries.GetBackgroundCheckQuery
{
    
    [RequestLogging]
    [RequestValidation]
    public class GetBackgroundCheckQueryHandler : IRequestHandler<GetBackgroundCheckQuery,
        Result<GetBackgroundCheckDto>>
    {

        private readonly ISqlRepository<Domain.Check.BackgroundCheck, int> _sqlRepository;
        private readonly IMapper _mapper;

        public GetBackgroundCheckQueryHandler(ISqlRepository<BackgroundCheck, int> sqlRepository, IMapper mapper)
        {
            _sqlRepository = sqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetBackgroundCheckDto>> Handle(GetBackgroundCheckQuery request, CancellationToken cancellationToken)
        {
            var check = await _sqlRepository.GetAsync(request.Id.Value, new string[]{nameof(BackgroundCheck.Approver)});

            if (check == null)
            {
                return Result.NotFound<GetBackgroundCheckDto>($"Couldn't find background check with provided identifier {request.Id.Value}");
            }

            var result = _mapper.Map<GetBackgroundCheckDto>(check);

            return Result.Ok(value: result);
        }
    }
}
