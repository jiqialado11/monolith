using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;
using SubContractors.Common.Redis;
using SubContractors.Domain.SubContractor;
using System.Collections.Generic;

namespace SubContractors.Application.Handlers.Staff.Commands.RetrieveAndCreateStaff
{
    public class RetrieveAndCreateStaff : IRequest<Result<int>>, IInvalidateCacheRequest
    {
        public int PmId { get; set; }
        public int SubContractorId { get; set; }

        public List<string> GetDomainsIdentifiers()
        {
            return new List<string> {
                                        typeof(SubContractor).FullName,
                                        typeof(Domain.SubContractor.Staff.Staff).FullName
                                     };
        }
    }

    public class RetrieveAndCreateStaffValidator : AbstractValidator<RetrieveAndCreateStaff>
    {
        public RetrieveAndCreateStaffValidator()
        {
            RuleFor(x => x.PmId).NotEmpty().WithMessage(Constants.ValidationErrors.Field_Is_Required);
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
