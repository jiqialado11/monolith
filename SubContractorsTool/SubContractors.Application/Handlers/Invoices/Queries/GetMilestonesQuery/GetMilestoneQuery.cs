using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;
using SubContractors.Common.Redis;
using System.Collections.Generic;

namespace SubContractors.Application.Handlers.Invoices.Queries.GetMilestonesQuery
{
    public class GetMilestoneQuery : IRequest<Result<List<GetMilestoneDto>>>, ICacheableRequest
    {
        public int? ProjectId { get; set; }

        public string GetDomainIdentifier()
        {
            return typeof(GetMilestoneDto).FullName;
        }
    }

    public class GetMailstoneValidation : AbstractValidator<GetMilestoneQuery>
    {
        public GetMailstoneValidation()
        {
            RuleFor(x => x.ProjectId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);
        }
    }
}
