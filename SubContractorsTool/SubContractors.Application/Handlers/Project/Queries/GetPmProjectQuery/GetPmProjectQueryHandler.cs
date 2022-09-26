using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Project;
using SubContractors.Infrastructure.ExternalServices.PmCoreSystem;

namespace SubContractors.Application.Handlers.Project.Queries.GetPmProjectQuery
{
    [RequestLogging]
    [RequestValidation]
    public class GetPmProjectQueryHandler : IRequestHandler<GetPmProjectQuery, Result<GetPmProjectDto>>
    {
        private readonly IPmCoreSystemService _pmCoreSystemService;
        private readonly IMapper _mapper;

        public GetPmProjectQueryHandler(IPmCoreSystemService pmCoreSystemService, IMapper mapper)
        {
            _pmCoreSystemService = pmCoreSystemService;
            _mapper = mapper;
        }

        public async Task<Result<GetPmProjectDto>> Handle(GetPmProjectQuery request, CancellationToken cancellationToken)
        {
            GetPmProjectDto result;

            var response = await _pmCoreSystemService.GetProjectDetailsAsync(request.Id.Value);

            if (!response.IsError)
            {
                if (response.Project != null)
                {
                    result = _mapper.Map<GetPmProjectDto>(response.Project);

                    result.Status = ProjectStatus.Started.ToString();
                    result.StatusId = (int)ProjectStatus.Started;
                }
                else
                {
                    return Result.NotFound<GetPmProjectDto>($"Couldn't retrieve project from PM System with provided identifier {request.Id}");
                }

            }
            else
            {
                return Result.Fail<GetPmProjectDto>(ResultType.InternalServerError, $"Exception during request execution, please find more details in application logs");
            }

            return Result.Ok(value: result);
        }
    }
}
