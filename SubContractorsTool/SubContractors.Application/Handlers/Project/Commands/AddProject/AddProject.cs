using System;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Project.Commands.AddProject
{
    public class AddProject : IRequest<Result<Guid>>
    {
        public int? PmId { get; set; }
        public int? SubContractorId { get; set; }
        public string ProjectName { get; set; }
        public int? ProjectGroupId { get; set; }
        public string ProjectGroup { get; set; }
        public int? ProjectManagerId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EstimatedFinishDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int? ProjectStatusId { get; set; }
    }

    public class AddProjectValidator : AbstractValidator<AddProject>
    {
        public AddProjectValidator()
        {
            RuleFor(x => x.PmId)
               .NotEmpty()
               .WithMessage(Constants.ValidationErrors.Field_Is_Required);
            RuleFor(x => x.SubContractorId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);
            RuleFor(x => x.ProjectName)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);
        }
    }
}