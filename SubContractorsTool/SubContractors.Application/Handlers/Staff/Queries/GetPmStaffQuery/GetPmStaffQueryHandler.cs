using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Common;
using SubContractors.Infrastructure.ExternalServices.PmCoreSystem;

namespace SubContractors.Application.Handlers.Staff.Queries.GetPmStaffQuery
{
    [RequestLogging]
    [RequestValidation]
    public class GetPmStaffQueryHandler : IRequestHandler<GetPmStaffQuery, Result<GetPmStaffDto>>
    {
        private readonly IPmCoreSystemService _pmCoreSystemService;
        private readonly ISqlRepository<Location, int> _sqlRepository;
        private readonly IMapper _mapper;

        public GetPmStaffQueryHandler(IPmCoreSystemService pmCoreSystemService, IMapper mapper, ISqlRepository<Location, int> sqlRepository)
        {
            _pmCoreSystemService = pmCoreSystemService;
            _mapper = mapper;
            _sqlRepository = sqlRepository;
        }

        public async Task<Result<GetPmStaffDto>> Handle(GetPmStaffQuery request, CancellationToken cancellationToken)
        {
            GetPmStaffDto result;

            var response = await _pmCoreSystemService.GetStaffDetailsAsync(request.Id.Value);
            if (!response.IsError)
            {
                if (response.Data != null)
                {
                    result = _mapper.Map<GetPmStaffDto>(response.Data);
                }
                else
                {
                    return Result.NotFound<GetPmStaffDto>($"Couldn't retrieve staff from PM System with provided identifier {request.Id}");
                }
            }
            else
            {
                return Result.Fail<GetPmStaffDto>(ResultType.InternalServerError, $"Exception during request execution, please find more details in application logs");
            }

            return Result.Ok(value: result);
        }
    }
}
