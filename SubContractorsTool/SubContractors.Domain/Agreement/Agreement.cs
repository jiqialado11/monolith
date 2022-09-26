using System;
using System.Collections.Generic;
using SubContractors.Common.EfCore;
using SubContractors.Domain.Budget;
using SubContractors.Domain.Common;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Domain.Agreement
{
    public class Agreement : Entity<int>
    {
        public Agreement()
        {
            
        }

        public Agreement(int id)
        {
            Id = id;
        }
        public string Title { get; set; }
        public string DocumentUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LegalEntity LegalEntity { get; set; }
        public SubContractor.SubContractor SubContractor { get; set; }
        public string Condition { get; set; }
        public Location BudgetOffice { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<Addendum> Addenda { get; set; }

        public void Create(string title, string url, DateTime startDate, DateTime endDate, string condition)
        {
            Title = title;
            DocumentUrl = url;
            StartDate = startDate;
            EndDate = endDate;
            Condition = condition;
        }

        public void Update(string title, string url, DateTime startDate, DateTime endDate, string condition)
        {
            Title = title;
            DocumentUrl = url;
            StartDate = startDate;
            EndDate = endDate;
            Condition = condition;
        }

        public void AssignBudgetOffice(Location office)
        {
            BudgetOffice = office;
        }

        public void AssignLegalEntity(LegalEntity legalEntity)
        {
            LegalEntity = legalEntity;
        }

        public void AssignPaymentMethod(PaymentMethod paymentMethod)
        {
            PaymentMethod = paymentMethod;
        }

        public void AssignAddendum(Addendum addendum)
        {
            Addenda ??= new List<Addendum>();

            if (!Addenda.Contains(addendum))
            {
                Addenda.Add(addendum);
            }
        }
    }
}