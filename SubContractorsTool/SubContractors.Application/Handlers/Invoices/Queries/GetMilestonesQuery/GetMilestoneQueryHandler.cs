using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Infrastructure.ExternalServices.PmAccounting;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Application.Handlers.Invoices.Queries.GetMilestonesQuery
{
    [RequestLogging]
    [RequestValidation]
    [RequestCashing]
    public class GetMilestoneQueryHandler : IRequestHandler<GetMilestoneQuery, Result<List<GetMilestoneDto>>>
    {
        private readonly IPmAccountingService _pmAccountService;
        private readonly IMapper _mapper;

        public GetMilestoneQueryHandler(IPmAccountingService pmClient,
                                        IMapper mapper)
        {
            _pmAccountService = pmClient;
            _mapper = mapper;
        }

        public async Task<Result<List<GetMilestoneDto>>> Handle(GetMilestoneQuery request, CancellationToken cancellationToken)
        {
            List<GetMilestoneDto> result;

            var response = await _pmAccountService.GetMilestonesAsync(request.ProjectId.Value);
            if (!response.IsError && response.Data != null)
            {
                if (response.Data.Milestones.Count > 0)
                {
                    result = response.Data.Milestones.Select(x => _mapper.Map<GetMilestoneDto>(x))
                                                     .ToList();
                }
                else
                {
                    return Result.NotFound<List<GetMilestoneDto>>($"milestones wasn't found with provided identifier {request.ProjectId}");
                }
            }
            else
            {
                return Result.Fail<List<GetMilestoneDto>>(ResultType.InternalServerError,$"Exception during request execution, please find more details in application logs");
            }

            return Result.Ok(value: result);
        }
    }
}
