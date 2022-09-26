using System;
using System.Collections.Generic;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;
using SubContractors.Common.Redis;
using SubContractors.Domain.Compliance;

namespace SubContractors.Application.Handlers.Compliance.Commands.UpdateCompliance
{
    public class UpdateCompliance : IRequest<Result<Unit>>, IInvalidateCacheRequest
    {
        public int? Id { get; set; }
        public int? TypeId { get; set; }
        public int? RatingId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Comment { get; set; }
        public Guid FileId { get; set; }

        public List<string> GetDomainsIdentifiers()
        {
            return new List<string> { 
                                      typeof(ComplianceRating).FullName,
                                      typeof(Domain.Compliance.Compliance).FullName
                                     };
        }
    }

    public class UpdateComplianceValidator : AbstractValidator<UpdateCompliance>
    {
        public UpdateComplianceValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

            RuleFor(x => x.TypeId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .ExclusiveBetween(0, 5)
                .WithMessage(Constants.ValidationErrors.Compliance_Type_Value_Range);


            RuleFor(x => x.ExpirationDate)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);


            RuleFor(x => x.FileId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);

            RuleFor(x => x.RatingId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

            RuleFor(x => x.Comment)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);
        }
    }
}
