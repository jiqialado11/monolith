using SubContractors.Common.EfCore;
using System;
using System.ComponentModel;

namespace SubContractors.Domain.Compliance
{
    public class Compliance : Entity<int>
    {
        public string Comment { get; set; }
        public DateTime ExpirationDate { get; set; }
        public ComplianceType Type { get; set; }
        public ComplianceRating Rating { get; set; }
        public ComplianceFile File { get; set; }
        public SubContractor.SubContractor SubContractor { get; set; }

        public void Create(string comment, DateTime expirationDate, ComplianceType type)
        {
            Comment = comment;
            ExpirationDate = expirationDate;
            Type = type;
        }

        public void Update(string comment, DateTime expirationDate, ComplianceType type)
        {
            Comment = comment;
            ExpirationDate = expirationDate;
            Type = type;
          
        }

        public void AddRating(ComplianceRating rating)
        {
            Rating = rating;
        }

        public void AddFile(ComplianceFile file)
        {
            File = file;
        }
     
    }

    public enum ComplianceType
    {
        [Description("Certificate")]
        Certificate = 1,
        [Description("Report")]
        Report,
        [Description("Assessment Form")]
        AssessmentForm,
        [Description("Other")]
        Other
    }
}