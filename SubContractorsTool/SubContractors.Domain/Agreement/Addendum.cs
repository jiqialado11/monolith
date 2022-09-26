using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SubContractors.Common.EfCore;
using SubContractors.Domain.Budget;
using SubContractors.Domain.Common;

namespace SubContractors.Domain.Agreement
{
    public class Addendum : Entity<int>
    {
        public Addendum()
        {
        }

        public Addendum(int id)
        {
            Id = id;
        }
        public string Title { get; set; }
        public string DocumentUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comment { get; set; }
        public PaymentTerm PaymentTerm { get; set; }
        public int PaymentTermInDays { get; set; }
        public bool IsRateForNonBillableProjects { get; set; }
        public Currency Currency { get; set; }
        public List<Rate> Rates { get; set; }
        public Agreement Agreement { get; set; }
        public List<Project.Project> Projects { get; set; }
        public List<SubContractor.Staff.Staff> Staffs { get; set; }
        public List<Invoice.Invoice> Invoices { get; set; }

        public void Create(string title, string url, DateTime startDate, DateTime endDate, string comment, int? paymentTermInDays, bool? isRateForNonBillableProjects)
        {
            Title = title;
            DocumentUrl = url;
            StartDate = startDate;
            EndDate = endDate;
            Comment = comment;
            if (paymentTermInDays.HasValue)
            {
                PaymentTermInDays = paymentTermInDays.Value;
            }

            if (isRateForNonBillableProjects.HasValue)
            {
                IsRateForNonBillableProjects = isRateForNonBillableProjects.Value;
            }
          
        }

        public void Update(string title, string url, DateTime startDate, DateTime endDate, string comment, int? paymentTermInDays, bool? isRateForNonBillableProjects)
        {
            Title = title;
            DocumentUrl = url;
            StartDate = startDate;
            EndDate = endDate;
            Comment = comment;
            if (paymentTermInDays.HasValue)
            {
                PaymentTermInDays = paymentTermInDays.Value;
            }

            if (isRateForNonBillableProjects.HasValue)
            {
                IsRateForNonBillableProjects = isRateForNonBillableProjects.Value;
            }
        }

        public void AssignPaymentTerm(PaymentTerm paymentTerm)
        {
            PaymentTerm = paymentTerm;
        }

        public void AssignCurrency(Currency currency)
        {
            Currency = currency;
        }
        public void AssignToAgreement(Agreement agreement)
        {
            Agreement = agreement;
        }

        public void AddProject(Project.Project project)
        {
            Projects ??= new List<Project.Project>();

            if (!Projects.Contains(project))
            {
                Projects.Add(project);
            }
        }

        public void ClearProjects()
        {
            if (Projects != null && Projects.Any())
            {
                Projects.Clear();
            }
        }
        public void AddRate(Rate rate)
        {
            Rates ??= new List<Rate>();

            if (!Rates.Contains(rate))
            {
                Rates.Add(rate);
            }
        }
    }
}