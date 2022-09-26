using SubContractors.Common.EfCore;
using System;
using System.ComponentModel;

namespace SubContractors.Domain.Invoice
{
    public class BudgetInfo : Entity<Guid>
    {
        public BudgetInfo()
        {

        }

        public BudgetInfo(Guid id)
        {
            Id = id;
        }

        public DateTime PaidDate { get; set; }
        public DateTime PlannedPaidDate { get; set; }
        public string BudgedRequestId { get; set; }
        public string BudgetInvoiceId { get; set; }
        public RequestStatus BudgetRequestStatus { get; set; }
        public int? InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }

    public enum RequestStatus
    {
        [Description("New")]
        New = 1,
        [Description("Deleted")]
        Deleted,
        [Description("Returned")]
        Returned,
        [Description("Revised")]
        Revised,
        [Description("Approved")]
        Approved,
        [Description("Rejected")]
        Rejected,
        [Description("Confirmed")]
        Confirmed
    }
}
