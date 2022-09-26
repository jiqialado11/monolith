using System;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Invoices.Queries.ExportInvoicesQuery
{
    public class ExportInvoicesQuery : IRequest<Result<ExportInvoicesDto>>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class ExportInvoicesQueryValidator: AbstractValidator<ExportInvoicesQuery>
    {
        public ExportInvoicesQueryValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .Must((model, field) => field <= model.EndDate)
                .WithMessage(Constants.ValidationErrors.StartDate_less_than_EndDate)
                .When(x=>x.EndDate.HasValue);

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .When(x=>x.StartDate.HasValue);
        }
    }
}
