using System;

namespace SubContractors.Application.Handlers.Check.Queries.GetBackgroundChecksQuery
{
    public class GetBackgroundChecksDto
    {
        public int? Id { get; set; }
        public int? CheckApproverId { get; set; }
        public string CheckApprover { get; set; }
        public int? CheckStatusId { get; set; }
        public string CheckStatus { get; set; }
        public DateTime? Date { get; set; }
        public string Link { get; set; }
    }
}
