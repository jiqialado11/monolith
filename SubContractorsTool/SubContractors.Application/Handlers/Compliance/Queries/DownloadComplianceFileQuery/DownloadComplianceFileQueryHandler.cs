using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;

namespace SubContractors.Application.Handlers.Compliance.Queries.DownloadComplianceFileQuery
{
    [RequestLogging]
    [RequestValidation]
    public class DownloadComplianceFileQueryHandler : IRequestHandler<DownloadComplianceFileQuery, Result<DownloadComplianceFileDto>>
    {
        private readonly ISqlRepository<Domain.Compliance.ComplianceFile, Guid> _sqlRepository;
        private readonly IMapper _mapper;

        public DownloadComplianceFileQueryHandler(IMapper mapper, ISqlRepository<Domain.Compliance.ComplianceFile, Guid> sqlRepository)
        {
            _mapper = mapper;
            _sqlRepository = sqlRepository;
        }

        public async Task<Result<DownloadComplianceFileDto>> Handle(DownloadComplianceFileQuery request, CancellationToken cancellationToken)
        {
            var file = await _sqlRepository.GetAsync(request.Id.Value, new string[] {});
            if (file == null)
            {
                return Result.NotFound<DownloadComplianceFileDto>($"Couldn't find file with provided identifier {request.Id.Value}");
            }

            var result = _mapper.Map<DownloadComplianceFileDto>(file);
            return Result.Ok(value: result);
        }
    }
}
