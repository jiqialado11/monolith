using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Infrastructure.ExternalServices.PmCoreSystem;

namespace SubContractors.Application.Handlers.Project.Queries.SearchPmProjectQuery
{
    [RequestLogging]
    [RequestValidation]
    public class SearchPmProjectQueryHandler : IRequestHandler<SearchPmProjectQuery, Result<IList<SearchPmProjectDto>>>
    {
        private readonly IPmCoreSystemService _pmCoreSystemService;
        private readonly IMapper _mapper;

        public SearchPmProjectQueryHandler(IPmCoreSystemService pmCoreSystemService, IMapper mapper)
        {
            _pmCoreSystemService = pmCoreSystemService;
            _mapper = mapper;
        }

        public async Task<Result<IList<SearchPmProjectDto>>> Handle(SearchPmProjectQuery request, CancellationToken cancellationToken)
        {
            IList<SearchPmProjectDto> result;

            var response = await _pmCoreSystemService.GetProjectListAsync();
            if (!response.IsError)
            {
                if (response.Data != null && response.Data.Any())
                {
                    result = response.Data.Select(x => _mapper.Map<SearchPmProjectDto>(x))
                        .ToList();
                }
                else
                {
                    return Result.NotFound<IList<SearchPmProjectDto>>($"Couldn't retrieve project list from PM System");
                }
            }
            else
            {
                return Result.Fail<IList<SearchPmProjectDto>>(ResultType.InternalServerError, $"Exception during request execution, please find more details in application logs");
            }

            return Result.Ok(value: result);
        }
    }
}
