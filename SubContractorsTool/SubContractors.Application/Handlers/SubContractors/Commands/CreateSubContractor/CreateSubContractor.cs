using System;
using System.Collections.Generic;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;
using SubContractors.Common.Redis;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Handlers.SubContractors.Commands.CreateSubContractor
{
    public class CreateSubContractor : IRequest<Result<int>>, IInvalidateCacheRequest
    {
        public string Name { get; set; }
        public int? SubContractorType { get; set; }
        public int? SubContractorStatus { get; set; }
        public bool? IsNdaSigned { get; set; }
        public int? LocationId { get; set; }
        public string Skills { get; set; }
        public string Comment { get; set; }
        public string Contact { get; set; }
        public DateTime? LastInteractionDate { get; set; }
        public string CompanySite { get; set; }
        public int? SalesOfficeId { get; set; }
        public int? DevelopmentOfficeId { get; set; }
        public string Materials { get; set; }
        public int? MarketId { get; set; }

        public List<string> GetDomainsIdentifiers()
        {
            return new List<string> {
                                        typeof(SubContractor).FullName,
                                     };
        }
    }

    public class CreateSubContractorValidator : AbstractValidator<CreateSubContractor>
    {
        public CreateSubContractorValidator()
        {
            RuleFor(x => x.SubContractorType)
               .NotEmpty()
               .WithMessage(Constants.ValidationErrors.Field_Is_Required)
               .ExclusiveBetween(0, 8)
               .WithMessage(Constants.ValidationErrors.SubContractor_Type_Value_Range);

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);

            RuleFor(x => x.Contact)
               .NotEmpty()
               .WithMessage(Constants.ValidationErrors.Field_Is_Required);

            RuleFor(x => x.SubContractorStatus)
               .NotEmpty()
               .WithMessage(Constants.ValidationErrors.Field_Is_Required)
               .ExclusiveBetween(0, 5)
               .WithMessage(Constants.ValidationErrors.SubContractor_Status_Value_Range);

            RuleFor(x => x.LocationId)
               .NotEmpty()
               .WithMessage(Constants.ValidationErrors.Field_Is_Required)
               .GreaterThanOrEqualTo(1)
               .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
               .LessThanOrEqualTo(x => int.MaxValue)
               .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);               

            RuleFor(x => x.Skills)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);

            RuleFor(x => x.Comment)
               .NotEmpty()
               .WithMessage(Constants.ValidationErrors.Field_Is_Required);

            RuleFor(x => x.LastInteractionDate)
               .NotEmpty()
               .WithMessage(Constants.ValidationErrors.Field_Is_Required);

            RuleFor(x => x.DevelopmentOfficeId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value); 

            RuleFor(x => x.SalesOfficeId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value); 

            RuleFor(x => x.CompanySite)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);

            RuleFor(x => x.MarketId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value); 

            RuleFor(x => x.Materials)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);
        }
    }
}