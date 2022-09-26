using System;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Project.Commands.DeAssignInvoiceApprover
{
    public class DeAssignInvoiceApprover : IRequest<Result<Unit>>
    {
        public Guid? ProjectId { get; set; }
    }

    public class DeAssignInvoiceApproverValidator : AbstractValidator<DeAssignInvoiceApprover>
    {
        public DeAssignInvoiceApproverValidator()
        {

            RuleFor(x => x.ProjectId).NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);
        }
    }
}
