using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubContractors.Domain.Project;

namespace SubContractors.Infrastructure.Persistence.TypeConfiguration
{
    public class ProjectGroupConfiguration : IEntityTypeConfiguration<ProjectGroup>
    {
        public void Configure(EntityTypeBuilder<ProjectGroup> builder)
        {
            builder.ToTable("ProjectGroup", "project");
            
        }
    }
}
