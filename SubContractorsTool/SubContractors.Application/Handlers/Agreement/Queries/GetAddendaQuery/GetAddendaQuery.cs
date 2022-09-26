using System.Collections.Generic;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Application.Handlers.Agreement.Queries.GetAddendumQuery;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Agreement.Queries.GetAddendaQuery
{
    public class GetAddendaQuery : IRequest<Result<IList<GetAddendumDto>>>
    {
        public int? SubContractorId { get; set; }
    }

    public class GetAddendaQueryValidator : AbstractValidator<GetAddendaQuery>
    {
        public GetAddendaQueryValidator()
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