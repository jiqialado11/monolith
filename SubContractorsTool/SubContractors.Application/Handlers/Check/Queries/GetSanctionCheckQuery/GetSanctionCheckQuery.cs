using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Check.Queries.GetSanctionCheckQuery
{
    public class GetSanctionCheckQuery : IRequest<Result<GetSanctionCheckDto>>
    {
        public int? Id { get; set; }
    }

    public class GetSanctionCheckQueryValidator : AbstractValidator<GetSanctionCheckQuery>
    {
        public GetSanctionCheckQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

        }
    }
}
