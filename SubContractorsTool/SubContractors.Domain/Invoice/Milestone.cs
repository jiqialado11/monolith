using SubContractors.Common.EfCore;
using System;

namespace SubContractors.Domain.Invoice
{
    public class Milestone : Entity<int>
    {
        public Milestone()
        {
        }

        public Milestone(int id)
        {
            Id = id;
        }

        public int PmId { get; set; }
        public int InvoiceId { get; set; }
        public double Amount { get; set; }
        public string  Name { get; set; }
        public DateTime ToDate { get; set; }

        public Invoice Invoice { get; set; }
    }
}