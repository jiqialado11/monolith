using System;
using System.Collections.Generic;
using SubContractors.Application.Handlers.Project.Queries.GetInternalProjectsListQuery;

namespace SubContractors.Application.Handlers.Agreement.Queries.GetAddendumQuery
{
    public class GetAddendumDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<GetInternalProjectsListDto> Projects { get; set; }
        public int? SubContractorId { get; set; }
        public int? AgreementId { get; set; }
        public string Agreement { get; set; }
        public bool? IsForNonBillableProjects { get; set; }
        public int? PaymentTermInDays { get; set; }
        public int? PaymentTermId { get; set; }
        public string PaymentTerm { get; set; }
        public int? CurrencyId { get; set; }
        public string Currency { get; set; }
        public int? LegalEntityId { get; set; }
        public string LegalEntity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DocUrl { get; set; }
        public string ParentDocUrl { get; set; }
        public string Comment { get; set; }
    }
}