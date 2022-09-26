﻿using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;
using SubContractors.Common.Redis;
using SubContractors.Domain.Invoice;
using System;
using System.Collections.Generic;

namespace SubContractors.Application.Handlers.Invoices.Commands.UpdateInvoice
{
    public class UpdateInvoice : IRequest<Result<Unit>>, IInvalidateCacheRequest
    {
        public int? Id { get; set; }
        public Guid InvoiceFileId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Amount { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal? TaxRate { get; set; }
        public string Comment { get; set; }
        public Guid? ProjectId { get; set; }
        public int? AddendumId { get; set; }
        public bool IsUseInvoiceDateForBudgetSystem { get; set; }
        public IList<Guid> SupportingDocumentsIds { get; set; }

        public List<string> GetDomainsIdentifiers()
        {
            return new List<string> { 
                                        typeof(Invoice).FullName,
                                     };
        }
    }

    public class UpdateInvoiceValidator : AbstractValidator<UpdateInvoice>
    {
        public UpdateInvoiceValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

            RuleFor(x => x.ProjectId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);

            RuleFor(x => x.AddendumId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

            RuleFor(x => x.Amount)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .Must((model, field) => field <= model.EndDate)
                .WithMessage(Constants.ValidationErrors.StartDate_less_than_EndDate);

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);

            RuleFor(x => x.InvoiceDate)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);

            RuleFor(x => x.InvoiceFileId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);

            RuleFor(x => x.IsUseInvoiceDateForBudgetSystem)
                .NotNull()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);
        }
    }
}
