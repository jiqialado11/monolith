using Microsoft.EntityFrameworkCore;
using SubContractors.Domain.Budget;
using SubContractors.Domain.Check;
using SubContractors.Domain.Common;
using SubContractors.Domain.Compliance;
using SubContractors.Domain.Invoice;
using SubContractors.Domain.Project;
using SubContractors.Domain.Project.Task;
using SubContractors.Domain.SubContractor;
using SubContractors.Domain.SubContractor.Staff;
using SubContractors.Domain.SubContractor.Tax;
using System.Reflection;
using SubContractors.Domain.Agreement;

namespace SubContractors.Infrastructure.Persistence.EfCore
{
    public class SubContractorsDbContext : DbContext
    {
        public virtual DbSet<LegalEntity> LegalEntities { get; set; }
        public virtual DbSet<SubContractor> SubContractors { get; set; }
        public virtual DbSet<Market> Markets { get; set; }
        public virtual DbSet<Office> SubContractorOffices { get; set; }
        public virtual DbSet<BudgetGroup> BudgetGroups { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<PaymentTerm> PaymentTerms { get; set; }
        public virtual DbSet<Agreement> Agreements { get; set; }
        public virtual DbSet<Addendum> Addenda { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Rate> AddendumRates { get; set; }
        public virtual DbSet<RateUnit> RateTypes { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TimeSheet> TimeSheets { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectGroup> ProjectGroups { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Compliance> Compliances { get; set; }
        public virtual DbSet<ComplianceRating> ComplianceRatings { get; set; }
        public virtual DbSet<ComplianceFile> ComplianceFiles { get; set; }
        public virtual DbSet<SanctionCheck> SanctionChecks { get; set; }
        public virtual DbSet<BackgroundCheck> BackgroundChecks { get; set; }
        public virtual DbSet<SupportingDocument> SupportingDocumentation { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Milestone> MileStones { get; set; }
        public virtual DbSet<Tax> Taxes { get; set; }
        public virtual DbSet<TaxType> TaxTypes { get; set; }
               
        public SubContractorsDbContext(DbContextOptions<SubContractorsDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public SubContractorsDbContext()
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Rate>()
                        .ToTable("Rate", "agreements");
            modelBuilder.Entity<LegalEntity>()
                        .ToTable("LegalEntity", "subcontractors");
            modelBuilder.Entity<Agreement>()
                        .ToTable("Agreement", "agreements");
 
            modelBuilder.Entity<Tax>()
                        .ToTable("Tax", "tax");
            modelBuilder.Entity<Task>()
                        .ToTable("Task", "tasks");
            modelBuilder.Entity<TimeSheet>()
                        .ToTable("TimeSheet", "tasks");
            modelBuilder.Entity<Note>()
                        .ToTable("Note", "tasks");
            modelBuilder.Entity<Compliance>()
                        .ToTable("Compliance", "compliance");
            modelBuilder.Entity<ComplianceFile>()
                        .ToTable("ComplianceFile", "compliance");
            modelBuilder.Entity<ComplianceRating>()
                        .ToTable("ComplianceRating", "compliance");        
            modelBuilder.Entity<SupportingDocument>()
                        .ToTable("SupportingDocumentation", "invoice");
            modelBuilder.Entity<BudgetInfo>()
                        .ToTable("BudgedInfo", "invoice");

            modelBuilder.Entity<BudgetInfo>()
                        .HasOne(p => p.Invoice)
                        .WithOne(a => a.BudgedInfo)
                        .HasForeignKey<Invoice>(k => k.BudgedInfoId);

            // Note: Do following steps for all many-to-many relations to prevent 'Migration Loop' issue: https://github.com/dotnet/efcore/issues/23937
            modelBuilder.Entity<SubContractor>()
                        .HasMany(l => l.Markets)
                        .WithMany(r => r.SubContractors)
                        .UsingEntity(a => a.ToTable("MarketSubContractor", "subcontractors"));

            modelBuilder.Entity<SubContractor>()
                        .HasMany(l => l.Offices)
                        .WithMany(r => r.SubContractors)
                        .UsingEntity(a => a.ToTable("OfficeSubContractor", "subcontractors"));
        }
    }
}