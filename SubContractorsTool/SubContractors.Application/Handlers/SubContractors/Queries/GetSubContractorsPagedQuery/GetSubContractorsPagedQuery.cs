using System.Linq;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;
using SubContractors.Common.EfCore.Pagination;
using SubContractors.Common.Redis;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractorsPagedQuery
{
    public class GetSubContractorsPagedQuery : PagedQueryBase, IRequest<Result<PagedResult<GetSubContractorsPagedDto>>>, ICacheableRequest
    {
        public int? QueryType { get; set; }

        public string GetDomainIdentifier()
        {
            return typeof(SubContractor).FullName;
        }
    }

    public class GetSubContractorsQueryValidator : AbstractValidator<GetSubContractorsPagedQuery>
    {
        public GetSubContractorsQueryValidator()
        {
            RuleFor(x => x.QueryType)
               .NotEmpty()
               .WithMessage(Constants.ValidationErrors.Field_Is_Required)
               .Must(x => x != null && new[] {0, 1}.Contains(x.Value))
               .WithMessage(Constants.ValidationErrors.SubContractor_QueryType_Value_Range);
        }
    }

    public enum SubContractorQueryType
    {
        List,
        Library
    }
}