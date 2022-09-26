using SubContractors.Common.EfCore;

namespace SubContractors.Domain.Common
{
    public class Location : Entity<int>
    {
        public Location()
        {

        }

        public int? MdpId { get; set; }
        public int? LeaderPMID { get; set; }
        public int? CountryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string CountryCode { get; set; }
        public bool IsOnsite { get; set; }
        public bool IsProduction { get; set; }
        public string DefaultCurrencyCode { get; set; }
        public string TimezoneName { get; set; }       

    }
}