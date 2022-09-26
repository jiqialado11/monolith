using System;
using System.Collections.Generic;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;
using SubContractors.Common.Redis;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Handlers.Staff.Commands.UpdateStaff
{
    public class UpdateStaff : IRequest<Result<Unit>>, IInvalidateCacheRequest
    {
        public int? Id { get; set; }
        public int? PmId { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CellPhone { get; set; }
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

    public class UpdateStaffValidator : AbstractValidator<UpdateStaff>
    {
        public UpdateStaffValidator()
        {
            RuleFor(x => x.PmId).NotEmpty().WithMessage(Constants.ValidationErrors.Field_Is_Required);
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);
            
            RuleFor(x => x.DepartmentName).NotEmpty().WithMessage(Constants.ValidationErrors.Field_Is_Required);
            RuleFor(x => x.DomainLogin).NotEmpty().WithMessage(Constants.ValidationErrors.Field_Is_Required);
        }
    }
}
