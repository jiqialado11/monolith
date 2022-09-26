using SubContractors.Common.EfCore;
using SubContractors.Domain.Agreement;
using SubContractors.Domain.SubContractor.Staff;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SubContractors.Domain.Invoice
{
    public class Invoice : Entity<int>
    {
        public Invoice()
        {
            
        }

        public Invoice(int id)
        {
            Id = id;
        }
        public int? MilestoneId { get; set; }
        public int? ReferralId { get; set; }
        public SupportingDocument InvoiceFile { get; set; }
        public Guid? InvoiceFileId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int? PaymentNumber { get; set; }
        public decimal Amount { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public string Comment { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; }
        public SubContractor.SubContractor SubContractor { get; set; }
        public Project.Project Project { get; set; }
        public List<SupportingDocument> SupportingDocuments { get; set; }
        public Staff Referral { get; set; }
        public Addendum Addendum { get; set; }
        public Milestone MileStone { get; set; }
        public BudgetInfo BudgedInfo { get; set; }
        public Guid? BudgedInfoId { get; set; }
        public bool IsUseInvoiceDateForBudget { get; set; }

        public void CreateOrUpdateFields(decimal amount, DateTime startDate, DateTime endDate, DateTime invoiceDate, 
            string invoiceNumber, decimal taxRate, decimal taxAmount, string comment, int? invoiceStatus, bool isUseInvoiceDate)
        {
            Amount = amount;
            StartDate = startDate;
            EndDate = endDate;
            InvoiceDate = invoiceDate;
            InvoiceNumber = invoiceNumber;
            TaxRate = taxRate;
            TaxAmount = taxAmount;
            Comment = comment;
            IsUseInvoiceDateForBudget = isUseInvoiceDate;

            if (invoiceStatus.HasValue)
            {
                InvoiceStatus = (InvoiceStatus)invoiceStatus.Value;
            }
        }

        public void AssignAddendum(Addendum addendum)
        {
            Addendum = addendum;
        }

        public void AssignSubContractor(SubContractor.SubContractor subContractor)
        {
            SubContractor = subContractor;
        }

        public void AssignReferral(Staff referral)
        {
            Referral = referral;
        }

        public void AssignProject(Project.Project project)
        {
            Project = project;
        }

        public void AssignMilestone(Milestone milestone)
        {
            MileStone = milestone;
        }

        public void AddSupportingDocument(List<SupportingDocument> supportingDocuments)
        {
            SupportingDocuments ??= new List<SupportingDocument>();
            foreach (var file in supportingDocuments)
            {
                SupportingDocuments.Add(file);
            }
        }

        public void AddInvoiceFile(SupportingDocument invoiceFile)
        {
            InvoiceFile = invoiceFile;
        }

    }

    public enum InvoiceStatus
    {
        [Description("New")]
        New = 1,
        [Description("Reviewed")]
        Reviewed,
        [Description("Approved")]
        Approved,
        [Description("Rejected")]
        Rejected,
        [Description("Sent to pay")]
        SentToPay
    }
}