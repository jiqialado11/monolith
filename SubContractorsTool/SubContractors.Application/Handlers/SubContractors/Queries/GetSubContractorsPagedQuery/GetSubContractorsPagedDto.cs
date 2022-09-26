using System;
using System.Collections.Generic;
using SubContractors.Application.Handlers.SubContractors.Queries.GetMarketsQuery;
using SubContractors.Application.Handlers.SubContractors.Queries.GetOfficesQuery;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractorsPagedQuery
{
    public class GetSubContractorsPagedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Rating { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public string Skills { get; set; }
        public bool? IsNdaSigned { get; set; }
        public string Contact { get; set; }
        public DateTime LastInteractionDate { get; set; }
        public IReadOnlyList<GetBudgetOfficeDto> BudgetLocations { get; set; }
        public IReadOnlyList<GetPaymentTermDto> PaymentTerms { get; set; }
        public IList<GetSubContractorLegalEntityDto> LegalEntities { get; set; }
        public IList<GetSubContractorProjectGroupDto> ProjectGroups { get; set; }
    }

    public class GetPaymentTermDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }

    public class GetBudgetOfficeDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }

    public class GetSubContractorProjectGroupDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class GetSubContractorLegalEntityDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}