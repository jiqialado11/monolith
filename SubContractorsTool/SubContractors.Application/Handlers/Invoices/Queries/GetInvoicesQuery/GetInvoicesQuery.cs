using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;
using SubContractors.Common.Redis;
using SubContractors.Domain.Invoice;
using System.Collections.Generic;

namespace SubContractors.Application.Handlers.Invoices.Queries.GetInvoicesQuery
{
    public class GetInvoicesQuery : IRequest<Result<IList<GetInvoicesDto>>>, ICacheableRequest
    {
        public int? SubContractorId { get; set; }

        public string GetDomainIdentifier()
        {
            return typeof(Invoice).FullName;
        }
    }

    public class GetInvoicesValidator : AbstractValidator<GetInvoicesQuery>
    {
        public GetInvoicesValidator()
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