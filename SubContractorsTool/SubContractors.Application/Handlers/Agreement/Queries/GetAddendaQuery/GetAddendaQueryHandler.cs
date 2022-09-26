using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Application.Handlers.Agreement.Queries.GetAddendumQuery;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractors.Application.Handlers.Agreement.Queries.GetAddendaQuery
{
    [RequestLogging]
    [RequestValidation]
    public class GetAddendaQueryHandler : IRequestHandler<GetAddendaQuery, Result<IList<GetAddendumDto>>>
    {
        private readonly IAddendaSqlRepository _sqlRepository;
        private readonly ISqlRepository<Domain.Agreement.Agreement, int> _agreementSqlRepository;
        private readonly ISqlRepository<SubContractor, int> _subSqlRepository;
        private readonly IMapper _mapper;

        public GetAddendaQueryHandler(
            IAddendaSqlRepository sqlRepository, 
            ISqlRepository<Domain.Agreement.Agreement, int> agreementSqlRepository,
            ISqlRepository<SubContractor, int> subSqlRepository, 
            IMapper mapper)
        {
            _sqlRepository = sqlRepository;
            _agreementSqlRepository = agreementSqlRepository;
            _subSqlRepository = subSqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetAddendumDto>>> Handle(GetAddendaQuery request, CancellationToken cancellationToken)
        {
            var subContractor = await _subSqlRepository.GetAsync(request.SubContractorId.Value, new string[] {});
            if (subContractor == null)
            {
                return Result.NotFound<IList<GetAddendumDto>>($"Couldn't find subContractor with provided identifier {request.SubContractorId.Value}");
            }

            var agreements = await _agreementSqlRepository.FindAsync(
                x => x.SubContractor.Id == request.SubContractorId.Value,
                new[] { nameof(Domain.Agreement.Agreement.SubContractor), nameof(Domain.Agreement.Agreement.LegalEntity) });

            if (agreements == null || !agreements.Any())
            {
                return Result.NotFound<IList<GetAddendumDto>>($"SubContractor with identifier {request.SubContractorId.Value} doesn't have agreements or addendums");
            }

            
            var addenda = await _sqlRepository.FindAsync(x => agreements.Select(a => a.Id)
                .ToList().Contains(x.Agreement.Id));

            if (addenda == null || !addenda.Any())
            {
                return Result.NotFound<IList<GetAddendumDto>>($"SubContractor with identifier {request.SubContractorId.Value} doesn't have addenda");
            }


            IList<GetAddendumDto> result = addenda.Select(s => _mapper.Map<GetAddendumDto>(s))
                                                   .ToList();

            return Result.Ok(value: result);
        }
    }
}