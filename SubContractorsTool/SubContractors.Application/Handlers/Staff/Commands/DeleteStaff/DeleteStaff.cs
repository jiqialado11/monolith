using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;
using SubContractors.Common.Redis;
using SubContractors.Domain.SubContractor;
using System.Collections.Generic;

namespace SubContractors.Application.Handlers.Staff.Commands.DeleteStaff
{
    public class DeleteStaff : IRequest<Result<Unit>>, IInvalidateCacheRequest
    {
        public int? SubContractorId { get; set; }
        public int? StaffId { get; set; }

        public List<string> GetDomainsIdentifiers()
        {
            return new List<string> {
                                        typeof(SubContractor).FullName,
                                        typeof(Domain.SubContractor.Staff.Staff).FullName
                                     };
        }
    }

    public class DeleteStaffValidator : AbstractValidator<DeleteStaff>
    {
        public DeleteStaffValidator()
        {
            RuleFor(x => x.SubContractorId).NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

            RuleFor(x => x.StaffId).NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);
        }
    }
}
