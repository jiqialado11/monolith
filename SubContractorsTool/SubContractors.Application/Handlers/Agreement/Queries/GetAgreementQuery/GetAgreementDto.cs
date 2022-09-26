using System;

namespace SubContractors.Application.Handlers.Agreement.Queries.GetAgreementQuery
{
    public class GetAgreementDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? SubContractorId { get; set; }
        public string SubContractor { get; set; }
        public int? LegalEntityId { get; set; }
        public string LegalEntity { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? LocationId { get; set; }
        public string Location { get; set; }
        public string Conditions { get; set; }
        public int? PaymentMethodId { get; set; }
        public string PaymentMethod { get; set; }
        public string Url { get; set; }
    }
}