using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubContractors.Domain.Compliance;
using System.Collections.Generic;

namespace SubContractors.Infrastructure.Persistence.TypeConfiguration
{
    public class ComplianceRatingConfiguration : IEntityTypeConfiguration<ComplianceRating>
    {
        public void Configure(EntityTypeBuilder<ComplianceRating> builder)
        {
            builder.ToTable("ComplianceRating", "compliance");

            var list = new List<ComplianceRating>
            {
                new(1, "A", "The level of conformance with DataArt information security, compliance and legal requirements is good. " + "Subcontractor may be engaged into client’s projects with the consideration of specific client’s requirements."),
                new(2, "B", "The level of conformance with DataArt information security, compliance and legal requirements is sufficient. " + "Subcontractor may be engaged into client’s projects without being provided with privileged access rights (e.g. access to production). " + "Additional internal discussion may be required depending on the scope of the planned services."),
                new(3, "C", "The level of conformance with DataArt information security, compliance and legal requirements is insufficient. " + "Subcontractor may be engaged into client’s projects only after additional internal discussion and applying necessary security controls."),
                new(4, "D", "The level of conformance with information security, compliance and legal requirements is insufficient as they are not applicable " + "to the nature of subcontractor’s business. Subcontractor may be engaged into non-client facing projects (e.g. internal, consultancy).")
            };
            builder.HasData(list);
        }
    }
}