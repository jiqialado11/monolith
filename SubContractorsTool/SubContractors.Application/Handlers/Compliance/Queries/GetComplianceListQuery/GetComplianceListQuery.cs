using System.Collections.Generic;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;
using SubContractors.Common.Redis;

namespace SubContractors.Application.Handlers.Compliance.Queries.GetComplianceListQuery
{
    public class GetComplianceListQuery:IRequest<Result<IList<GetComplianceDto>>>, ICacheableRequest
    {
        public int? SubContractorId { get; set; }

        public string GetDomainIdentifier()
        {
            return typeof(Domain.Compliance.Compliance).FullName;
        }
    }

    public class GetComplianceListQueryValidator : AbstractValidator<GetComplianceListQuery>
    {
        public GetComplianceListQueryValidator()
        {
            RuleFor(x => x.SubContractorId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);
        }
    }
}
