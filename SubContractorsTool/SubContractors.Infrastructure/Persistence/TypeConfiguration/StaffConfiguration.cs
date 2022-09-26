using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubContractors.Domain.SubContractor.Staff;

namespace SubContractors.Infrastructure.Persistence.TypeConfiguration
{
    public class StaffConfiguration: IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("Staff", "staff")
                .Property(x => x.FirstName)
                .IsRequired();
            builder
                .Property(x => x.LastName)
                .IsRequired();
        }
    }
}
