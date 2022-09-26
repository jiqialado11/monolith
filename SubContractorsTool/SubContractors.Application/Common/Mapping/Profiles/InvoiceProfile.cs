using AutoMapper;
using SubContractors.Application.Common.Helpers.Export;
using SubContractors.Application.Handlers.Invoices.Queries.GetAllInvoicesPagedQuery;
using SubContractors.Application.Handlers.Invoices.Queries.GetInvoiceQuery;
using SubContractors.Application.Handlers.Invoices.Queries.GetInvoicesQuery;
using SubContractors.Application.Handlers.Invoices.Queries.GetInvoiceStatusesQuery;
using SubContractors.Application.Handlers.Invoices.Queries.GetMilestonesQuery;
using SubContractors.Common.Extensions;
using SubContractors.Domain.Invoice;
using SubContractors.Domain.SubContractor.Staff;
using System;

namespace SubContractors.Application.Common.Mapping.Profiles
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<Invoice, GetInvoiceDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.StartDate, o => o.MapFrom(source => source.StartDate))
                .ForMember(dest => dest.EndDate, o => o.MapFrom(source => source.EndDate))
                .ForMember(dest => dest.InvoiceDate, o => o.MapFrom(source => source.InvoiceDate))
                .ForMember(dest => dest.ProjectId, o => o.MapFrom(source => source.Project.Id))
                .ForMember(dest => dest.Referral, o => o.MapFrom(source => source.Referral))                
                .ForMember(dest => dest.AddendumId, o => o.MapFrom(source => source.Addendum == null ? default : source.Addendum.Id))
                .ForMember(dest => dest.SubcontractorId, o => o.MapFrom(source => source.SubContractor.Id))
                .ForMember(dest => dest.MileStoneId, o => o.MapFrom(source => source.MileStone == null ? default : source.MileStone.Id))
                .ForMember(dest => dest.PaymentNumber, o => o.MapFrom(source => source.PaymentNumber == null ? default : source.PaymentNumber))
                .ForMember(dest => dest.isUseInvoiceDateForBudgetSystem, o => o.MapFrom(source => source.IsUseInvoiceDateForBudget))
                .ForMember(dest => dest.BudgetInfo, o => o.MapFrom(source => source.BudgedInfo))
                .ForMember(dest => dest.AgreementId, o => o.MapFrom(source => source.Addendum.Agreement == null ? default : source.Addendum.Agreement.Id))
                .ForMember(dest => dest.Amount, o => o.MapFrom(source => source.Amount))
                .ForMember(dest => dest.TaxAmount, o => o.MapFrom(source => source.TaxAmount))
                .ForMember(dest => dest.TaxRate, o => o.MapFrom(source => source.TaxRate))
                .ForMember(dest => dest.Comment, o => o.MapFrom(source => source.Comment))
                .ForMember(dest => dest.Currency, o => o.MapFrom(source => source.Addendum.Currency.Code))
                .ForMember(dest => dest.InvoiceNumber, o => o.MapFrom(source => source.InvoiceNumber))
                .ForMember(dest => dest.InvoiceStatusId, o => o.MapFrom(source => (int)source.InvoiceStatus))
                .ForMember(dest => dest.InvoiceStatus, o => o.MapFrom(source => source.InvoiceStatus.GetDescription()))
                .ForMember(dest => dest.InvoiceFile, o => o.MapFrom(source => source.InvoiceFile))
                .ForMember(dest => dest.SupportingDocuments, o => o.MapFrom(source => source.SupportingDocuments));

            CreateMap<Staff, GetReferralDto>()
                .ForMember(dest => dest.ReferralId, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.FirstName, o => o.MapFrom(source => source.FirstName))
                .ForMember(dest => dest.StartDate, o => o.MapFrom(source => source.StartDate))
                .ForMember(dest => dest.LastName, o => o.MapFrom(source => source.LastName));

            CreateMap<BudgetInfo, GetBudgetDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.BudgetInvoiceId, o => o.MapFrom(source => source.BudgetInvoiceId))
                .ForMember(dest => dest.PaidDate, o => o.MapFrom(source => source.PaidDate))
                .ForMember(dest => dest.BudgetInvoiceStatusId, o => o.MapFrom(source => (int)source.BudgetRequestStatus))
                .ForMember(dest => dest.BudgetInvoiceStatus, o => o.MapFrom(source => source.BudgetRequestStatus.GetDescription()))
                .ForMember(dest => dest.PlannedPaidDate, o => o.MapFrom(source => source.PlannedPaidDate));

            CreateMap<SupportingDocument, GetFileDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, o => o.MapFrom(source => source.Filename));

            CreateMap<Invoice, GetInvoicesDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.ReferralId, o => o.MapFrom(source => source.ReferralId))
                .ForMember(dest => dest.BudgedInfoId, o => o.MapFrom(source => source.BudgedInfoId))
                .ForMember(dest => dest.MileStoneId, o => o.MapFrom(source => source.MilestoneId))
                .ForMember(dest => dest.AddendumId, o => o.MapFrom(source => source.Addendum.Id))
                .ForMember(dest => dest.Amount, o => o.MapFrom(source => source.Amount))
                .ForMember(dest => dest.TaxAmount, o => o.MapFrom(source => source.TaxAmount))
                .ForMember(dest => dest.Currency, o => o.MapFrom(source => source.Addendum.Currency.Code))
                .ForMember(dest => dest.StartDate, o => o.MapFrom(source => source.StartDate))
                .ForMember(dest => dest.EndDate, o => o.MapFrom(source => source.EndDate))
                .ForMember(dest => dest.InvoiceNumber, o => o.MapFrom(source => source.InvoiceNumber))
                .ForMember(dest => dest.InvoiceStatusId, o => o.MapFrom(source => (int)source.InvoiceStatus))
                .ForMember(dest => dest.InvoiceStatus, o => o.MapFrom(source => source.InvoiceStatus.GetDescription()))
                .ForMember(dest => dest.Project, o => o.MapFrom(source => source.Project.Name))
                .ForMember(dest => dest.IsUseInvoiceDateForBudgetSystem, o => o.MapFrom(source => source.IsUseInvoiceDateForBudget))
                .ForMember(dest => dest.ProjectId, o => o.MapFrom(source => source.Project.Id));

            CreateMap<Invoice, GetAllInvoicesPagedDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.SubcontractorId, o => o.MapFrom(source => source.SubContractor.Id))
                .ForMember(dest => dest.PaymentNumber, o => o.MapFrom(source => source.Referral == null ? default : source.PaymentNumber.Value))
                .ForMember(dest => dest.ReferralId, o => o.MapFrom(source => source.Referral == null ? default : source.Referral.Id))
                .ForMember(dest => dest.Amount, o => o.MapFrom(source => source.Amount))
                .ForMember(dest => dest.CurrencyCode, o => o.MapFrom(source => source.Addendum.Currency.Code))
                .ForMember(dest => dest.EndDate, o => o.MapFrom(source => source.EndDate))
                .ForMember(dest => dest.StartDate, o => o.MapFrom(source => source.StartDate))
                .ForMember(dest => dest.InvoiceDate, o => o.MapFrom(source => source.InvoiceDate))
                .ForMember(dest => dest.InvoiceNumber, o => o.MapFrom(source => source.InvoiceNumber))
                .ForMember(dest => dest.InvoiceStatus, o => o.MapFrom(source => source.InvoiceStatus.GetDescription()))
                .ForMember(dest => dest.InvoiceStatusId, o => o.MapFrom(source => (int)source.InvoiceStatus))
                .ForMember(dest => dest.PaidDate, o => o.MapFrom(source => source.BudgedInfo.PaidDate))
                .ForMember(dest => dest.PlannedPaidDate, o => o.MapFrom(source => source.BudgedInfo.PlannedPaidDate))
                .ForMember(dest => dest.SubContractorTypeId, o => o.MapFrom(source => (int)source.SubContractor.SubContractorType))
                .ForMember(dest => dest.SubContractorType, o => o.MapFrom(source => source.SubContractor.SubContractorType.GetDescription()))
                .ForMember(dest => dest.LegalEntity, o => o.MapFrom(source => source.Addendum.Agreement.LegalEntity.EnglishName))
                .ForMember(dest => dest.SubContractorName, o => o.MapFrom(source => source.SubContractor.Name))
                .ForMember(dest => dest.IsUseInvoiceDateForBudgetSystem, o => o.MapFrom(source => source.IsUseInvoiceDateForBudget))
                .ForMember(dest => dest.AccountManagerName,
                           o => o.MapFrom(source => $"{source.SubContractor.AccountManager.FirstName} {source.SubContractor.AccountManager.LastName}"))
                .ForMember(dest => dest.ApproverName,
                           o => o.MapFrom(source => $"{source.Project.InvoiceApprover.FirstName} {source.Project.InvoiceApprover.LastName}"))
                .ForMember(dest => dest.ProjectId, o => o.MapFrom(source => source.Project.Id));

            CreateMap<Infrastructure.ExternalServices.PmAccounting.ResponseModels.Milestone, GetMilestoneDto>()
                .ForMember(dest => dest.PmId, o => o.MapFrom(source => source.MilestoneId))
                .ForMember(dest => dest.Name, o => o.MapFrom(source => source.MilestoneName))
                .ForMember(dest => dest.ToDate, o => o.MapFrom(source => DateTime.Parse(source.MilestoneToDate)))
                .ForMember(dest => dest.Amount, o => o.MapFrom(source => source.Amount));

            CreateMap<Invoice, InvoiceExportModel>()
                .ForMember(dest => dest.StartDate, o => o.MapFrom(source => source.StartDate))
                .ForMember(dest => dest.EndDate, o => o.MapFrom(source => source.EndDate))
                .ForMember(dest => dest.InvoiceDate, o => o.MapFrom(source => source.InvoiceDate))
                .ForMember(dest => dest.PaymentNumber, o => o.MapFrom(source => source.PaymentNumber))
                .ForMember(dest => dest.TaxRate, o => o.MapFrom(source => source.TaxRate))
                .ForMember(dest => dest.TaxAmount, o => o.MapFrom(source => source.TaxAmount))
                .ForMember(dest => dest.Comment, o => o.MapFrom(source => source.Comment))
                .ForMember(dest => dest.Number, o => o.MapFrom(source => source.InvoiceNumber))
                .ForMember(dest => dest.Status, o => o.MapFrom(source => source.InvoiceStatus.GetDescription()))
                .ForMember(dest => dest.Vendor, o => o.MapFrom(source => source.SubContractor.Name));

            CreateMap<InvoiceStatus, GetInvoiceStatusesDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => (int)source))
                .ForMember(dest => dest.Value, o => o.MapFrom(source => source.GetDescription()));
        }
    }
}
