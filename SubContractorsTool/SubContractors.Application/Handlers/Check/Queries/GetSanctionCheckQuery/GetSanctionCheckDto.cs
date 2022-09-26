using System;

namespace SubContractors.Application.Handlers.Check.Queries.GetSanctionCheckQuery
{
    public class GetSanctionCheckDto
    { 
        public int? Id { get; set; }
        public int? CheckApproverId { get; set; }
        public string CheckApprover { get; set; }
        public int? CheckStatusId { get; set; }
        public string CheckStatus { get; set; }
        public DateTime? Date { get; set; }
        public string Comment { get; set; }
    }
}
