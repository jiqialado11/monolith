using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractorsPagedQuery;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractors.Application.Handlers.Agreement.Queries.GetAgreementsQuery
{
    [RequestLogging]
    [RequestValidation]
    public class GetAgreementsQueryHandler : IRequestHandler<GetAgreementsQuery, Result<IList<GetAgreementsDto>>>
    {
        private readonly IAgreementSqlRepository _agreementSqlRepository;
        private readonly ISqlRepository<SubContractor, int> _subSqlRepository;
        private readonly IMapper _mapper;

        public GetAgreementsQueryHandler(ISqlRepository<SubContractor, int> subSqlRepository, IAgreementSqlRepository agreementSqlRepository, IMapper mapper)
        {
            _subSqlRepository = subSqlRepository;
            _agreementSqlRepository = agreementSqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetAgreementsDto>>> Handle(GetAgreementsQuery request, CancellationToken cancellationToken)
        {
            var subContractor = await _subSqlRepository.GetAsync(request.SubContractorId.Value);
            if (subContractor == null)
            {
                return Result.NotFound<IList<GetAgreementsDto>>($"Couldn't find subContractor with provided identifier {request.SubContractorId.Value}");
            }

            var agreements = await _agreementSqlRepository.FindAsync(x => x.SubContractor.Id == request.SubContractorId.Value);

            if (agreements == null || !agreements.Any())
            {
                return Result.NotFound<IList<GetAgreementsDto>>($"SubContractor with identifier {request.SubContractorId.Value} doesn't have agreements or addenda");
            }

            IList<GetAgreementsDto> result = agreements.Select(x => _mapper.Map<GetAgreementsDto>(x))
                                                       .ToList();

            foreach (var agreement in agreements)
            {
                if (agreement.Addenda == null || !agreement.Addenda.Any())
                {
                    continue;
                }

                foreach (var addendum in agreement.Addenda)
                {
                    var matchedAgreement = result.FirstOrDefault(x => x.Id == agreement.Id);
                    if (matchedAgreement != null && matchedAgreement.PaymentTerms == null)
                    {
                        matchedAgreement.PaymentTerms = new List<GetPaymentTermDto>();
                        matchedAgreement.PaymentTerms.Add(_mapper.Map<GetPaymentTermDto>(addendum.PaymentTerm));
                    }

                }
            }

            return Result.Ok(value: result);
        }
    }
}