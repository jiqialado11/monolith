using SubContractors.Common.EfCore;

namespace SubContractors.Domain.SubContractor
{
    public class LegalEntity : Entity<int>
    {
        public int? MdpId { get; set; }
        public string EnglishName { get; set; }
        public string HeadPositionEnglishName { get; set; }
        public string HeadPositionLocalLanguageName { get; set; }
        public string TaxNumber { get; set; }
        public string LegalRegistrationCode { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string AddressInEnglish { get; set; }
        public string AddressInLocalLanguage { get; set; }
        public bool IsActive { get; set; }
        public int VersionId { get; set; }
    }
}