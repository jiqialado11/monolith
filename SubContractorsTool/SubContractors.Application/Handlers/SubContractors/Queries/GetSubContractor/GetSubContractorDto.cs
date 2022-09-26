using System;
using System.Collections.Generic;
using SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractorsStatusesQuery;
using SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractorsTypeQuery;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractor
{
    public class GetSubContractorDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public string Location { get; set; }
        public string Comment { get; set; }
        public DateTime LastInteractionDate { get; set; }
        public bool IsNdaSigned { get; set; }
        public string CompanySite { get; set; }
        public string Contact { get; set; }
        public string Skills { get; set; }
        public string Materials { get; set; }
        public GetSubContractorTypesDto SubContractorType { get; set; }
        public GetSubContractorsStatusesDto SubContractorStatus { get; set; }
        public GetAccountManagerDto AccountManager { get; set; }
        public IReadOnlyList<GetSubContractorOfficeDto> SalesOffices { get; set; }
        public IReadOnlyList<GetSubContractorOfficeDto> DevelopmentOffices { get; set; }
        public IReadOnlyList<GetSubContractorMarketDto> Markets { get; set; }
    }

    public class GetAccountManagerDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }

    public class GetSubContractorMarketDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class GetSubContractorOfficeDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}