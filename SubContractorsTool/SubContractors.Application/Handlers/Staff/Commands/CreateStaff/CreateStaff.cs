using System;
using System.Collections.Generic;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;
using SubContractors.Common.Redis;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Handlers.Staff.Commands.CreateStaff
{
    public class CreateStaff : IRequest<Result<int>>, IInvalidateCacheRequest
    {
        public int? PmId { get; set; }
        public int? SubContractorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
        public string Position { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Qualifications { get; set; }
        public string RealLocation { get; set; }
        public string CellPhone { get; set; }
        public bool? IsNdaSigned { get; set; }
        public string DepartmentName { get; set; }
        public string DomainLogin { get; set; }

        public List<string> GetDomainsIdentifiers()
        {
            return new List<string> {
                                        typeof(SubContractor).FullName,
                                        typeof(Domain.SubContractor.Staff.Staff).FullName
                                     };
        }
    }

    public class CreateStaffValidator : AbstractValidator<CreateStaff>
    {
        public CreateStaffValidator()
        {
            RuleFor(x => x.PmId).NotEmpty().WithMessage(Constants.ValidationErrors.Field_Is_Required);
            RuleFor(x => x.SubContractorId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

            RuleFor(x => x.FirstName).NotEmpty().WithMessage(Constants.ValidationErrors.Field_Is_Required);
            RuleFor(x => x.LastName).NotEmpty().WithMessage(Constants.ValidationErrors.Field_Is_Required);
            RuleFor(x => x.DepartmentName).NotEmpty().WithMessage(Constants.ValidationErrors.Field_Is_Required);
            RuleFor(x => x.DomainLogin).NotEmpty().WithMessage(Constants.ValidationErrors.Field_Is_Required);
            RuleFor(x => x.Position).NotEmpty().WithMessage(Constants.ValidationErrors.Field_Is_Required);
        }
    }
}