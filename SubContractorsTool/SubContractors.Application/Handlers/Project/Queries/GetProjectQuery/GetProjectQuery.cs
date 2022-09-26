using System;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Project.Queries.GetProjectQuery
{
    public class GetProjectQuery : IRequest<Result<GetProjectQueryDto>>
    {
        public Guid? Id { get; set; }
    }

    public class GetProjectQueryValidator : AbstractValidator<GetProjectQuery>
    {
        public GetProjectQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);

        }
    }
}
