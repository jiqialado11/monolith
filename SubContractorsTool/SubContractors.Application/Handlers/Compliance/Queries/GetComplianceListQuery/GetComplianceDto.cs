using System;
using System.Collections.Generic;

namespace SubContractors.Application.Handlers.Compliance.Queries.GetComplianceListQuery
{
    public class GetComplianceDto
    {
        public int? Id { get; set; }

        public int? SubcontractorId { get; set; }
        public GetComplianceFileDto File { get; set; }

        public int? ComplianceTypeId { get; set; }
        public string ComplianceType { get; set; }
        public int? ComplianceRatingId { get; set; }
        public string ComplianceRating { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Comment { get; set; }

    }

    public class GetComplianceFileDto
    {
        public Guid? Id { get; set; }
        public string Filename { get; set; }
    }
}
