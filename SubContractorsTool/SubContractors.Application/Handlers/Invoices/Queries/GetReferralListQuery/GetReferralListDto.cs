using System;

namespace SubContractors.Application.Handlers.Invoices.Queries.GetReferralListQuery
{
    public class GetReferralListDto
    {
        public int ReferralId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
