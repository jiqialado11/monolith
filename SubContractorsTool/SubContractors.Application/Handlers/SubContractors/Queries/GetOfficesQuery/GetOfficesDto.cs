using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetOfficesQuery
{
    public class GetOfficesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OfficeType OfficeType { get; set; }
    }
}