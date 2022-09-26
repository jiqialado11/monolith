using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.EfCore.Pagination;
using SubContractors.Common.Extensions;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Budget;
using SubContractors.Domain.Compliance;
using SubContractors.Domain.SubContractor;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SubContractors.Domain.Common;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractorsPagedQuery
{
    [RequestLogging]
    [RequestValidation]
    [RequestCashing]
    public class GetSubContractorsPagedQueryHandler : IRequestHandler<GetSubContractorsPagedQuery, Result<PagedResult<GetSubContractorsPagedDto>>>
    {
        private readonly ISqlRepository<SubContractor, int> _subContractorSqlRepository;
        private readonly IMapper _mapper;

        public GetSubContractorsPagedQueryHandler(ISqlRepository<SubContractor, int> subContractorSqlRepository,
                                                  IMapper mapper)
        {
            _subContractorSqlRepository = subContractorSqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<PagedResult<GetSubContractorsPagedDto>>> Handle(GetSubContractorsPagedQuery request, CancellationToken cancellationToken)
        {

            var pagedResult = await _subContractorSqlRepository.BrowseAsync(
                x => (SubContractorQueryType)request.QueryType == SubContractorQueryType.List ?
                              (x.SubContractorStatus == SubContractorStatus.Active ||
                               x.SubContractorStatus == SubContractorStatus.InActive) :
                (x.SubContractorStatus == SubContractorStatus.Active ||
                 x.SubContractorStatus == SubContractorStatus.InActive ||
                 x.SubContractorStatus == SubContractorStatus.Tentative),
                request,
                source => (SubContractorQueryType)request.QueryType == SubContractorQueryType.Library ?
                    source.Include(x => x.Location)
                    :
                    source.Include(x => x.Projects)
                        .ThenInclude(p => p.ProjectGroup)
                        .Include(x => x.Agreements)
                        .ThenInclude(a => a.LegalEntity)
                        .Include(x => x.Location)
                        .Include(x => x.Agreements)
                        .ThenInclude(a => a.BudgetOffice)
                        .Include(x => x.Agreements)
                        .ThenInclude(a => a.Addenda)
                        .ThenInclude(ad => ad.PaymentTerm)
                        .Include(x=>x.Compliances)
                        .ThenInclude(c=>c.Rating)
            );

            if (pagedResult.IsEmpty)
            {
                return Result.NotFound<PagedResult<GetSubContractorsPagedDto>>("Couldn't find entities with provided parameters");
            }

            var subContractorsList = pagedResult.Items.Select(c => new GetSubContractorsPagedDto
            {
                Id = c.Id,
                Name = c.Name,
                Type = c.SubContractorType.GetDescription(),
                Location = c.Location.Name,
                Status =  c.SubContractorStatus.GetDescription() ,
                Skills = (SubContractorQueryType)request.QueryType == SubContractorQueryType.Library ? c.Skills : default,
                Comment = (SubContractorQueryType)request.QueryType == SubContractorQueryType.Library ? c.Comment : default,
                Contact = (SubContractorQueryType)request.QueryType == SubContractorQueryType.Library ? c.Contact : default,
                LastInteractionDate = (SubContractorQueryType)request.QueryType == SubContractorQueryType.Library ? c.LastInteractionDate : default,
                Description = (SubContractorQueryType)request.QueryType == SubContractorQueryType.List ?  c.Description : default,
                IsNdaSigned = (SubContractorQueryType)request.QueryType == SubContractorQueryType.List ? c.IsNDASigned : default,
                LegalEntities = (SubContractorQueryType)request.QueryType == SubContractorQueryType.List ? GetLegalEntities(c) : default,
                BudgetLocations = (SubContractorQueryType)request.QueryType == SubContractorQueryType.List ? GetBudgetLocations(c) : default,
                PaymentTerms = (SubContractorQueryType)request.QueryType == SubContractorQueryType.List ? GetPaymentTerms(c) : default,
                ProjectGroups = (SubContractorQueryType)request.QueryType == SubContractorQueryType.List ? GetProjectGroups(c) : default,
                Rating = (SubContractorQueryType)request.QueryType == SubContractorQueryType.List ? GetRating(c) : default
            });

            return Result.Ok(value: PagedResult<GetSubContractorsPagedDto>.From(pagedResult, subContractorsList));
        }

        private List<GetSubContractorLegalEntityDto> GetLegalEntities(SubContractor subContractor)
        {
            return subContractor.Agreements.Where(x => x.LegalEntity != null)
                                           .Select(x => _mapper.Map<Domain.SubContractor.LegalEntity, GetSubContractorLegalEntityDto>(x.LegalEntity))
                                           .ToList();
        }

        private List<GetBudgetOfficeDto> GetBudgetLocations(SubContractor subContractor)
        {
            return subContractor.Agreements.Where(x => x.BudgetOffice != null)
                                           .Select(x => _mapper.Map<Location, GetBudgetOfficeDto>(x.BudgetOffice))
                                           .ToList();
        }

        private List<GetPaymentTermDto> GetPaymentTerms(SubContractor subContractor)
        {
            return subContractor.Agreements.Where(x => x.Addenda != null && x.Addenda.Any())
                                           .SelectMany(x => x.Addenda.Select(y => y.PaymentTerm))
                                           .Select(x => _mapper.Map<PaymentTerm, GetPaymentTermDto>(x))
                                           .ToList();
        }

        private List<GetSubContractorProjectGroupDto> GetProjectGroups(SubContractor subContractor)
        {
            return subContractor.Projects.Where(x => x.ProjectGroup != null)
                                         .Select(x => _mapper.Map<Domain.Project.ProjectGroup, GetSubContractorProjectGroupDto>(x.ProjectGroup))
                                         .ToList();
        }

        private string GetRating(SubContractor subContractor)
        {
            return subContractor.Compliances.FirstOrDefault(x => x.Type == ComplianceType.AssessmentForm)
                                           ?.Rating.Value;
        }
    }
}