﻿using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Agreement.Commands.Delete_Addendum
{
    public class DeleteAddendum : IRequest<Result<Unit>>
    {
        public int? Id { get; set; }
    }

    public class DeleteAddendumValidator : AbstractValidator<DeleteAddendum>
    {
        public DeleteAddendumValidator()
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
