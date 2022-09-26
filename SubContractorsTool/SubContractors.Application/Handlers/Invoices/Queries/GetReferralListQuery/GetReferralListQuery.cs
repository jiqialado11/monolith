using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;
using System.Collections.Generic;

namespace SubContractors.Application.Handlers.Invoices.Queries.GetReferralListQuery
{
    public class GetReferralListQuery : IRequest<Result<List<GetReferralListDto>>>
    {
        public int? PaymentNumber { get; set; }
    }

    public class GetReferralListQueryValidation : AbstractValidator<GetReferralListQuery>
    {
        public GetReferralListQueryValidation()
        {
            RuleFor(x => x.PaymentNumber)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .ExclusiveBetween(0, 3)
                .WithMessage(Constants.ValidationErrors.Invoice_PaymentNumber_Value_Range);
        }
    }
}
