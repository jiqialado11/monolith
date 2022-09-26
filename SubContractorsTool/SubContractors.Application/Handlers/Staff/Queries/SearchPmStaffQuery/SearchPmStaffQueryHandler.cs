using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Infrastructure.ExternalServices.PmCoreSystem;

namespace SubContractors.Application.Handlers.Staff.Queries.SearchPmStaffQuery
{
    [RequestLogging]
    [RequestValidation]
    public class SearchPmStaffQueryHandler : IRequestHandler<SearchPmStaffQuery, Result<IList<SearchPmStaffDto>>>
    {
        private readonly IPmCoreSystemService _pmCoreSystemService;
        private readonly IMapper _mapper;

        public SearchPmStaffQueryHandler(IPmCoreSystemService pmCoreSystemService, IMapper mapper)
        {
            _pmCoreSystemService = pmCoreSystemService;
            _mapper = mapper;
        }

        public async Task<Result<IList<SearchPmStaffDto>>> Handle(SearchPmStaffQuery request, CancellationToken cancellationToken)
        {
            IList<SearchPmStaffDto> result;

            var response = await _pmCoreSystemService.GetStaffListAsync();
            if (!response.IsError)
            {
                if (response.StaffContractorList != null && response.StaffContractorList.Any())
                {
                    result = response.StaffContractorList.Select(x => _mapper.Map<SearchPmStaffDto>(x))
                        .ToList();
                }
                else
                {
                    return Result.NotFound<IList<SearchPmStaffDto>>($"Couldn't retrieve staff list from PM System");
                }
            }
            else
            {
                return Result.Fail<IList<SearchPmStaffDto>>(ResultType.InternalServerError, $"Exception during request execution, please find more details in application logs");
            }

            return Result.Ok(value: result);
        }
    }
}
