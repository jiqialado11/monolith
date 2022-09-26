using System;
using System.Collections.Generic;
using SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractorsPagedQuery;

namespace SubContractors.Application.Handlers.Agreement.Queries.GetAgreementsQuery
{
    public class GetAgreementsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Conditions { get; set; }
        public int? LegalEntityId { get; set; }
        public string LegalEntity { get; set; }
        public string Url { get; set; }
        public int? LocationId { get; set; }
        public string Location { get; set; }
        public int? PaymentMethodId { get; set; }
        public string PaymentMethod { get; set; }
        public IList<GetPaymentTermDto> PaymentTerms { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}