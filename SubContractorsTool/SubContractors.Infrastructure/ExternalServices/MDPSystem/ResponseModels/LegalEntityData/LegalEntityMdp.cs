using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SubContractors.Infrastructure.ExternalServices.MDPSystem.ResponseModels.LegalEntityData
{
    public class LegalEntityMdp
    {
        [JsonProperty("versions")]
        public List<string> Versions { get; set; }

        [JsonProperty("versionId")]
        public int VersionId { get; set; }

        [JsonProperty("entityId")]
        public int EntityId { get; set; }

        [JsonProperty("modifiedByUserId")]
        public int ModifiedByUserId { get; set; }

        [JsonProperty("modifiedBySystemId")]
        public int ModifiedBySystemId { get; set; }

        [JsonProperty("actualStartDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("actualFinishDate")]
        public DateTime FinishDate { get; set; }

        [JsonProperty("entityIsDeleted")]
        public bool EntityIsDeleted { get; set; }

        [JsonProperty("versionIsDeleted")]
        public bool VersionIsDeleted { get; set; }

        [JsonProperty("rn")]
        public int RN { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("englishName")]
        public string EnglishName { get; set; }

        [JsonProperty("localLanguageName")]
        public string LocalLanguageName { get; set; }

        [JsonProperty("addressInEnglish")]
        public string AddressInEnglish { get; set; }

        [JsonProperty("addressInLocalLanguage")]
        public string AddressInLocalLanguage { get; set; }

        [JsonProperty("cityName")]
        public string CityName { get; set; }

        [JsonProperty("countryName")]
        public string CountryName { get; set; }

        [JsonProperty("defaultCurrencyCode")]
        public string DefaultCurrencyCode { get; set; }

        [JsonProperty("headEnglishName")]
        public string HeadEnglishName { get; set; }

        [JsonProperty("headLocalLanguageName")]
        public string HeadLocalLanguageName { get; set; }

        [JsonProperty("headPositionEnglishName")]
        public string HeadPositionEnglishName { get; set; }

        [JsonProperty("headPositionLocalLanguageName")]
        public string HeadPositionLocalLanguageName { get; set; }

        [JsonProperty("legalRegistrationCode")]
        public string LegalRegistrationCode { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("taxNumber")]
        public string TaxNumber { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isTaxReturned")]
        public bool IsTaxReturned { get; set; }

        [JsonProperty("corporation")]
        public bool Corporation { get; set; }

        [JsonProperty("shareholders")]
        public string Shareholders { get; set; }

        [JsonProperty("responsibleAccounterPmId")]
        public int ResponsibleAccounterPmId { get; set; }

        [JsonProperty("authorizedСapital")]
        public string AuthorizedСapital { get; set; }

        [JsonProperty("responsibleAccounterName")]
        public string ResponsibleAccounterName { get; set; }

        [JsonProperty("mainAccountingSystems")]
        public string MainAccountingSystems { get; set; }

        [JsonProperty("consolidationAccountingSystem")]
        public string ConsolidationAccountingSystem { get; set; }

        [JsonProperty("isRealLE")]
        public bool IsRealLE { get; set; }

        [JsonProperty("isBilledByEntrepreneurs")]
        public bool IsBilledByEntrepreneurs { get; set; }

        [JsonProperty("isVAR")]
        public bool IsVAR { get; set; }

        [JsonProperty("doesExpenseAllow")]
        public bool DoesExpenseAllow { get; set; }

        [JsonProperty("doesOperateCoworkings")]
        public bool DoesOperateCoworkings { get; set; }

        [JsonProperty("isSSC")]
        public bool IsSSC { get; set; }

        [JsonProperty("doesPayCommission")]
        public bool DoesPayCommission { get; set; }

        [JsonProperty("isVATApplied")]
        public bool IsVATApplied { get; set; }

        [JsonProperty("doesSignCustomerContract")]
        public bool DoesSignCustomerContract { get; set; }

        [JsonProperty("isCurrentVersion")]
        public bool IsCurrentVersion { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
