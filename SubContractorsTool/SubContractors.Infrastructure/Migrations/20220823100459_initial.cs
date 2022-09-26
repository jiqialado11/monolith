using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubContractors.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "agreements");

            migrationBuilder.EnsureSchema(
                name: "checks");

            migrationBuilder.EnsureSchema(
                name: "invoice");

            migrationBuilder.EnsureSchema(
                name: "budget");

            migrationBuilder.EnsureSchema(
                name: "compliance");

            migrationBuilder.EnsureSchema(
                name: "common");

            migrationBuilder.EnsureSchema(
                name: "subcontractors");

            migrationBuilder.EnsureSchema(
                name: "tasks");

            migrationBuilder.EnsureSchema(
                name: "project");

            migrationBuilder.EnsureSchema(
                name: "staff");

            migrationBuilder.EnsureSchema(
                name: "tax");

            migrationBuilder.CreateTable(
                name: "BudgedInfo",
                schema: "invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlannedPaidDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BudgedRequestId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BudgetInvoiceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BudgetRequestStatus = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgedInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BudgetGroup",
                schema: "budget",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BudgetGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsFunction = table.Column<bool>(type: "bit", nullable: false),
                    ParentBudgetGroupId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetGroup_BudgetGroup_ParentBudgetGroupId",
                        column: x => x.ParentBudgetGroupId,
                        principalSchema: "budget",
                        principalTable: "BudgetGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComplianceRating",
                schema: "compliance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplianceRating", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                schema: "common",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BudgetSystemId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LegalEntity",
                schema: "subcontractors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MdpId = table.Column<int>(type: "int", nullable: true),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadPositionEnglishName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadPositionLocalLanguageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LegalRegistrationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressInLocalLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    VersionId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                schema: "common",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MdpId = table.Column<int>(type: "int", nullable: true),
                    LeaderPMID = table.Column<int>(type: "int", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOnsite = table.Column<bool>(type: "bit", nullable: false),
                    IsProduction = table.Column<bool>(type: "bit", nullable: false),
                    DefaultCurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimezoneName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Market",
                schema: "subcontractors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Market", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Milestone",
                schema: "invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PmId = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Milestone", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Office",
                schema: "subcontractors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeType = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Office", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                schema: "budget",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BudgetSystemId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTerm",
                schema: "budget",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTerm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RateUnit",
                schema: "agreements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxType",
                schema: "tax",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addendum",
                schema: "agreements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTermId = table.Column<int>(type: "int", nullable: true),
                    PaymentTermInDays = table.Column<int>(type: "int", nullable: false),
                    IsRateForNonBillableProjects = table.Column<bool>(type: "bit", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    AgreementId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addendum", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addendum_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "common",
                        principalTable: "Currency",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addendum_PaymentTerm_PaymentTermId",
                        column: x => x.PaymentTermId,
                        principalSchema: "budget",
                        principalTable: "PaymentTerm",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AddendumProject",
                columns: table => new
                {
                    AddendaId = table.Column<int>(type: "int", nullable: false),
                    ProjectsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddendumProject", x => new { x.AddendaId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_AddendumProject_Addendum_AddendaId",
                        column: x => x.AddendaId,
                        principalSchema: "agreements",
                        principalTable: "Addendum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddendumStaff",
                columns: table => new
                {
                    AddendaId = table.Column<int>(type: "int", nullable: false),
                    StaffsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddendumStaff", x => new { x.AddendaId, x.StaffsId });
                    table.ForeignKey(
                        name: "FK_AddendumStaff_Addendum_AddendaId",
                        column: x => x.AddendaId,
                        principalSchema: "agreements",
                        principalTable: "Addendum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Agreement",
                schema: "agreements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LegalEntityId = table.Column<int>(type: "int", nullable: true),
                    SubContractorId = table.Column<int>(type: "int", nullable: true),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BudgetOfficeId = table.Column<int>(type: "int", nullable: true),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agreement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agreement_LegalEntity_LegalEntityId",
                        column: x => x.LegalEntityId,
                        principalSchema: "subcontractors",
                        principalTable: "LegalEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Agreement_Location_BudgetOfficeId",
                        column: x => x.BudgetOfficeId,
                        principalSchema: "common",
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Agreement_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalSchema: "budget",
                        principalTable: "PaymentMethod",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BackgroundCheck",
                schema: "checks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApproverId = table.Column<int>(type: "int", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckStatus = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: true),
                    SubContractorId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackgroundCheck", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Compliance",
                schema: "compliance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    RatingId = table.Column<int>(type: "int", nullable: true),
                    SubContractorId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compliance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compliance_ComplianceRating_RatingId",
                        column: x => x.RatingId,
                        principalSchema: "compliance",
                        principalTable: "ComplianceRating",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComplianceFile",
                schema: "compliance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Filename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComplianceId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplianceFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplianceFile_Compliance_ComplianceId",
                        column: x => x.ComplianceId,
                        principalSchema: "compliance",
                        principalTable: "Compliance",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                schema: "invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MilestoneId = table.Column<int>(type: "int", nullable: true),
                    ReferralId = table.Column<int>(type: "int", nullable: true),
                    InvoiceFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentNumber = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceStatus = table.Column<int>(type: "int", nullable: false),
                    SubContractorId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AddendumId = table.Column<int>(type: "int", nullable: true),
                    BudgedInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsUseInvoiceDateForBudget = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Addendum_AddendumId",
                        column: x => x.AddendumId,
                        principalSchema: "agreements",
                        principalTable: "Addendum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Invoice_BudgedInfo_BudgedInfoId",
                        column: x => x.BudgedInfoId,
                        principalSchema: "invoice",
                        principalTable: "BudgedInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoice_Milestone_MilestoneId",
                        column: x => x.MilestoneId,
                        principalSchema: "invoice",
                        principalTable: "Milestone",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SupportingDocumentation",
                schema: "invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Filename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    InvoiceId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportingDocumentation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportingDocumentation_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "invoice",
                        principalTable: "Invoice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MarketSubContractor",
                schema: "subcontractors",
                columns: table => new
                {
                    MarketsId = table.Column<int>(type: "int", nullable: false),
                    SubContractorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketSubContractor", x => new { x.MarketsId, x.SubContractorsId });
                    table.ForeignKey(
                        name: "FK_MarketSubContractor_Market_MarketsId",
                        column: x => x.MarketsId,
                        principalSchema: "subcontractors",
                        principalTable: "Market",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                schema: "tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfficeSubContractor",
                schema: "subcontractors",
                columns: table => new
                {
                    OfficesId = table.Column<int>(type: "int", nullable: false),
                    SubContractorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficeSubContractor", x => new { x.OfficesId, x.SubContractorsId });
                    table.ForeignKey(
                        name: "FK_OfficeSubContractor_Office_OfficesId",
                        column: x => x.OfficesId,
                        principalSchema: "subcontractors",
                        principalTable: "Office",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Project",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PmId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectGroupId = table.Column<int>(type: "int", nullable: true),
                    DeliveryManagerId = table.Column<int>(type: "int", nullable: true),
                    ProjectManagerId = table.Column<int>(type: "int", nullable: true),
                    InvoiceApproverId = table.Column<int>(type: "int", nullable: true),
                    BudgetGroupId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_BudgetGroup_BudgetGroupId",
                        column: x => x.BudgetGroupId,
                        principalSchema: "budget",
                        principalTable: "BudgetGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectGroup",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PmId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeliveryManagerId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectStaff",
                columns: table => new
                {
                    ProjectsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStaff", x => new { x.ProjectsId, x.StaffsId });
                    table.ForeignKey(
                        name: "FK_ProjectStaff_Project_ProjectsId",
                        column: x => x.ProjectsId,
                        principalSchema: "project",
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectSubContractor",
                columns: table => new
                {
                    ProjectsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubContractorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSubContractor", x => new { x.ProjectsId, x.SubContractorsId });
                    table.ForeignKey(
                        name: "FK_ProjectSubContractor_Project_ProjectsId",
                        column: x => x.ProjectsId,
                        principalSchema: "project",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rate",
                schema: "agreements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RateValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitId = table.Column<int>(type: "int", nullable: true),
                    AddendumId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rate_Addendum_AddendumId",
                        column: x => x.AddendumId,
                        principalSchema: "agreements",
                        principalTable: "Addendum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rate_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "project",
                        principalTable: "Project",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rate_RateUnit_UnitId",
                        column: x => x.UnitId,
                        principalSchema: "agreements",
                        principalTable: "RateUnit",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SanctionCheck",
                schema: "checks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApproverId = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckStatus = table.Column<int>(type: "int", nullable: false),
                    SubContractorId = table.Column<int>(type: "int", nullable: true),
                    StaffId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanctionCheck", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                schema: "staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PmId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Skype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CannotLoginBefore = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CannotLoginAfter = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Qualifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    RealLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CellPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsNdaSigned = table.Column<bool>(type: "bit", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DomainLogin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BudgetOfficeId = table.Column<int>(type: "int", nullable: true),
                    TaskId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staff_Location_BudgetOfficeId",
                        column: x => x.BudgetOfficeId,
                        principalSchema: "common",
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Staff_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "common",
                        principalTable: "Location",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubContractor",
                schema: "subcontractors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MdpId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastInteractionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsNDASigned = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    AccountManagerId = table.Column<int>(type: "int", nullable: true),
                    SubContractorType = table.Column<int>(type: "int", nullable: false),
                    SubContractorStatus = table.Column<int>(type: "int", nullable: false),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Materials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubContractor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubContractor_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "common",
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubContractor_Staff_AccountManagerId",
                        column: x => x.AccountManagerId,
                        principalSchema: "staff",
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Task",
                schema: "tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    IsOnSiteTask = table.Column<bool>(type: "bit", nullable: false),
                    IsKarmaTimeSheetsAllowed = table.Column<bool>(type: "bit", nullable: false),
                    PMOrDMCanEdit = table.Column<bool>(type: "bit", nullable: false),
                    SummaryTime = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EffortsSpent = table.Column<int>(type: "int", nullable: false),
                    EstimatedEfforts = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentTaskId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaskType = table.Column<int>(type: "int", nullable: false),
                    TaskStatus = table.Column<int>(type: "int", nullable: false),
                    ResponsiblePersonId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "project",
                        principalTable: "Project",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Task_Staff_ResponsiblePersonId",
                        column: x => x.ResponsiblePersonId,
                        principalSchema: "staff",
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Task_Task_ParentTaskId",
                        column: x => x.ParentTaskId,
                        principalSchema: "tasks",
                        principalTable: "Task",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StaffSubContractor",
                columns: table => new
                {
                    StaffsId = table.Column<int>(type: "int", nullable: false),
                    SubContractorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffSubContractor", x => new { x.StaffsId, x.SubContractorsId });
                    table.ForeignKey(
                        name: "FK_StaffSubContractor_Staff_StaffsId",
                        column: x => x.StaffsId,
                        principalSchema: "staff",
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaffSubContractor_SubContractor_SubContractorsId",
                        column: x => x.SubContractorsId,
                        principalSchema: "subcontractors",
                        principalTable: "SubContractor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tax",
                schema: "tax",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaxTypeId = table.Column<int>(type: "int", nullable: true),
                    SubContractorId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tax", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tax_SubContractor_SubContractorId",
                        column: x => x.SubContractorId,
                        principalSchema: "subcontractors",
                        principalTable: "SubContractor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tax_TaxType_TaxTypeId",
                        column: x => x.TaxTypeId,
                        principalSchema: "tax",
                        principalTable: "TaxType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TimeSheet",
                schema: "tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    ParentTimeSheetId = table.Column<int>(type: "int", nullable: true),
                    TimeSheetType = table.Column<int>(type: "int", nullable: false),
                    SpentTimeHours = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSheet_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "staff",
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimeSheet_Task_TaskId",
                        column: x => x.TaskId,
                        principalSchema: "tasks",
                        principalTable: "Task",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimeSheet_TimeSheet_ParentTimeSheetId",
                        column: x => x.ParentTimeSheetId,
                        principalSchema: "tasks",
                        principalTable: "TimeSheet",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "budget",
                table: "BudgetGroup",
                columns: new[] { "Id", "BudgetGroupName", "EmailAddress", "IsActive", "IsDefault", "IsDeleted", "IsFunction", "ParentBudgetGroupId" },
                values: new object[] { 32, "DataArt", "", false, false, false, false, null });

            migrationBuilder.InsertData(
                schema: "compliance",
                table: "ComplianceRating",
                columns: new[] { "Id", "Description", "IsDeleted", "Value" },
                values: new object[,]
                {
                    { 1, "The level of conformance with DataArt information security, compliance and legal requirements is good. Subcontractor may be engaged into client’s projects with the consideration of specific client’s requirements.", false, "A" },
                    { 2, "The level of conformance with DataArt information security, compliance and legal requirements is sufficient. Subcontractor may be engaged into client’s projects without being provided with privileged access rights (e.g. access to production). Additional internal discussion may be required depending on the scope of the planned services.", false, "B" },
                    { 3, "The level of conformance with DataArt information security, compliance and legal requirements is insufficient. Subcontractor may be engaged into client’s projects only after additional internal discussion and applying necessary security controls.", false, "C" },
                    { 4, "The level of conformance with information security, compliance and legal requirements is insufficient as they are not applicable to the nature of subcontractor’s business. Subcontractor may be engaged into non-client facing projects (e.g. internal, consultancy).", false, "D" }
                });

            migrationBuilder.InsertData(
                schema: "subcontractors",
                table: "Market",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, false, "Russia" },
                    { 2, false, "USA" },
                    { 3, false, "Ukraine" }
                });

            migrationBuilder.InsertData(
                schema: "subcontractors",
                table: "Office",
                columns: new[] { "Id", "IsDeleted", "Name", "OfficeType" },
                values: new object[,]
                {
                    { 1, false, "sample sales office", 1 },
                    { 2, false, "sample development office", 2 }
                });

            migrationBuilder.InsertData(
                schema: "budget",
                table: "PaymentTerm",
                columns: new[] { "Id", "IsActive", "IsDeleted", "Value" },
                values: new object[,]
                {
                    { 1, true, false, "NoRestriction" },
                    { 2, true, false, "AfterClientPayOnly" },
                    { 3, true, false, "AfterClientPayOnlyOrExpirationDate" }
                });

            migrationBuilder.InsertData(
                schema: "agreements",
                table: "RateUnit",
                columns: new[] { "Id", "IsDeleted", "Value" },
                values: new object[,]
                {
                    { 1, false, "per/hour" },
                    { 2, false, "per/day" },
                    { 3, false, "per/week" },
                    { 4, false, "per/month" },
                    { 5, false, "per/year" }
                });

            migrationBuilder.InsertData(
                schema: "tax",
                table: "TaxType",
                columns: new[] { "Id", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, "", false, "W9" },
                    { 2, "", false, "W-8BEN" }
                });

            migrationBuilder.InsertData(
                schema: "budget",
                table: "BudgetGroup",
                columns: new[] { "Id", "BudgetGroupName", "EmailAddress", "IsActive", "IsDefault", "IsDeleted", "IsFunction", "ParentBudgetGroupId" },
                values: new object[] { 33, "Functions", "", false, false, false, false, 32 });

            migrationBuilder.InsertData(
                schema: "budget",
                table: "BudgetGroup",
                columns: new[] { "Id", "BudgetGroupName", "EmailAddress", "IsActive", "IsDefault", "IsDeleted", "IsFunction", "ParentBudgetGroupId" },
                values: new object[] { 34, "Practices", "", false, false, false, false, 32 });

            migrationBuilder.InsertData(
                schema: "budget",
                table: "BudgetGroup",
                columns: new[] { "Id", "BudgetGroupName", "EmailAddress", "IsActive", "IsDefault", "IsDeleted", "IsFunction", "ParentBudgetGroupId" },
                values: new object[,]
                {
                    { 1, "abC.Finance", "dataart.abC.Finance@dataart.com", true, false, false, false, 34 },
                    { 2, "abC.Travel", "dataart.abC.Travel@dataart.com", true, false, false, false, 34 },
                    { 3, "abC.Media", "dataart.abC.Media@dataart.com", true, false, false, false, 34 },
                    { 4, "abC.Telecom", "dataart.abC.Telecom@dataart.com", false, false, false, false, 34 },
                    { 5, "abC.Healthcare", "dataart.abC.Healthcare@dataart.com", true, false, false, false, 34 },
                    { 6, "abC.IoT", "dataart.abC.DeviceHiveGroup@dataart.com", false, false, false, false, 34 },
                    { 7, "abC.GeneralDelivery", "dataart.abC.DigitalTransformation@dataart.com", true, true, false, false, 34 },
                    { 8, "abC.BrandManagement", "dataart.abC.BrandManagement@dataart.com", true, false, false, false, 33 },
                    { 9, "abC.iGaming", "dataart.abC.iGaming@dataart.com", false, false, false, false, 34 },
                    { 10, "abC.Function.HRM", "dataart.abC.HRM@dataart.com", true, false, false, true, 33 },
                    { 11, "abC.Function.ITServices", "dataart.abC.ITServices@dataart.com", true, false, false, true, 33 },
                    { 12, "abC.Sustainability", "dataart.abC.Ethnoexpert@dataart.com", false, false, false, false, 33 },
                    { 13, "abC.Function.DesignStudio", "dataart.abC.DesignStudio@dataart.com", false, false, false, true, 33 },
                    { 16, "abC.Function.ALF", "dataart.abC.Accounting@dataart.com", true, false, false, true, 33 },
                    { 17, "abC.Function.CorporateTravel", "dataart.abC.CorporateTravel@dataart.com", true, false, false, true, 33 },
                    { 18, "abC.Function.OBI", "dataart.abC.OBI@dataart.com", true, false, false, true, 33 },
                    { 19, "abC.Function.CG", "dataart.abc.CG@dataart.com", true, false, false, true, 33 },
                    { 20, "abC.Function.AM", "dataart.abc.AM@dataart.com", true, false, false, true, 33 },
                    { 21, "abC.Function.RM", "dataart.abc.ResourceMng@dataart.com", true, false, false, true, 33 },
                    { 22, "abC.Function.SI", "dataart.abc.SI@dataart.com", true, false, false, true, 33 },
                    { 23, "abC.Function.Null", "dataart.abc.ResourceMng@dataart.com", false, false, false, true, 33 },
                    { 24, "abC.Function.Compliance", "dataart.abc.Compliance@dataart.com", true, false, false, true, 33 },
                    { 25, "abC.Function.SER", "dataart.abc.SER@dataart.com", true, false, false, true, 33 },
                    { 39, "abC.Function.BPA", "dataart.abC.BPA@dataart.com", true, false, false, true, 33 },
                    { 40, "abC.Function.LD", "dataart.abc.LD@dataart.com", true, false, false, true, 33 }
                });

            migrationBuilder.InsertData(
                schema: "budget",
                table: "BudgetGroup",
                columns: new[] { "Id", "BudgetGroupName", "EmailAddress", "IsActive", "IsDefault", "IsDeleted", "IsFunction", "ParentBudgetGroupId" },
                values: new object[,]
                {
                    { 14, "abC.SolutionDesign", "dataart.abC.SolutionDesign@dataart.com", false, false, false, false, 20 },
                    { 15, "abC.Blockchain", "dataart.abC.Blockchain@dataart.com", false, false, false, false, 20 },
                    { 35, "abC.Finance.Enterprise", "dataart.abC.Finance@dataart.com", true, false, false, false, 1 },
                    { 36, "abC.Finance.US", "dataart.abC.Finance@dataart.com", true, false, false, false, 1 },
                    { 37, "abC.Finance.UK", "dataart.abC.Finance@dataart.com", true, false, false, false, 1 },
                    { 38, "abC.Finance.EMEA", "dataart.abC.Finance@dataart.com", true, false, false, false, 1 },
                    { 41, "abC.Retail", "dataart.abC.DigitalTransformation@dataart.com", true, false, false, false, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addendum_AgreementId",
                schema: "agreements",
                table: "Addendum",
                column: "AgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_Addendum_CurrencyId",
                schema: "agreements",
                table: "Addendum",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Addendum_PaymentTermId",
                schema: "agreements",
                table: "Addendum",
                column: "PaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_AddendumProject_ProjectsId",
                table: "AddendumProject",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_AddendumStaff_StaffsId",
                table: "AddendumStaff",
                column: "StaffsId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_BudgetOfficeId",
                schema: "agreements",
                table: "Agreement",
                column: "BudgetOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_LegalEntityId",
                schema: "agreements",
                table: "Agreement",
                column: "LegalEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_PaymentMethodId",
                schema: "agreements",
                table: "Agreement",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_SubContractorId",
                schema: "agreements",
                table: "Agreement",
                column: "SubContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_BackgroundCheck_ApproverId",
                schema: "checks",
                table: "BackgroundCheck",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_BackgroundCheck_StaffId",
                schema: "checks",
                table: "BackgroundCheck",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_BackgroundCheck_SubContractorId",
                schema: "checks",
                table: "BackgroundCheck",
                column: "SubContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetGroup_ParentBudgetGroupId",
                schema: "budget",
                table: "BudgetGroup",
                column: "ParentBudgetGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Compliance_RatingId",
                schema: "compliance",
                table: "Compliance",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Compliance_SubContractorId",
                schema: "compliance",
                table: "Compliance",
                column: "SubContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplianceFile_ComplianceId",
                schema: "compliance",
                table: "ComplianceFile",
                column: "ComplianceId",
                unique: true,
                filter: "[ComplianceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_AddendumId",
                schema: "invoice",
                table: "Invoice",
                column: "AddendumId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_BudgedInfoId",
                schema: "invoice",
                table: "Invoice",
                column: "BudgedInfoId",
                unique: true,
                filter: "[BudgedInfoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_InvoiceFileId",
                schema: "invoice",
                table: "Invoice",
                column: "InvoiceFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_MilestoneId",
                schema: "invoice",
                table: "Invoice",
                column: "MilestoneId",
                unique: true,
                filter: "[MilestoneId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ProjectId",
                schema: "invoice",
                table: "Invoice",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ReferralId",
                schema: "invoice",
                table: "Invoice",
                column: "ReferralId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_SubContractorId",
                schema: "invoice",
                table: "Invoice",
                column: "SubContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketSubContractor_SubContractorsId",
                schema: "subcontractors",
                table: "MarketSubContractor",
                column: "SubContractorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_TaskId",
                schema: "tasks",
                table: "Note",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficeSubContractor_SubContractorsId",
                schema: "subcontractors",
                table: "OfficeSubContractor",
                column: "SubContractorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_BudgetGroupId",
                schema: "project",
                table: "Project",
                column: "BudgetGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_DeliveryManagerId",
                schema: "project",
                table: "Project",
                column: "DeliveryManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_InvoiceApproverId",
                schema: "project",
                table: "Project",
                column: "InvoiceApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectGroupId",
                schema: "project",
                table: "Project",
                column: "ProjectGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectManagerId",
                schema: "project",
                table: "Project",
                column: "ProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectGroup_DeliveryManagerId",
                schema: "project",
                table: "ProjectGroup",
                column: "DeliveryManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStaff_StaffsId",
                table: "ProjectStaff",
                column: "StaffsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSubContractor_SubContractorsId",
                table: "ProjectSubContractor",
                column: "SubContractorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Rate_AddendumId",
                schema: "agreements",
                table: "Rate",
                column: "AddendumId");

            migrationBuilder.CreateIndex(
                name: "IX_Rate_ProjectId",
                schema: "agreements",
                table: "Rate",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Rate_StaffId",
                schema: "agreements",
                table: "Rate",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Rate_UnitId",
                schema: "agreements",
                table: "Rate",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_SanctionCheck_ApproverId",
                schema: "checks",
                table: "SanctionCheck",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_SanctionCheck_StaffId",
                schema: "checks",
                table: "SanctionCheck",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_SanctionCheck_SubContractorId",
                schema: "checks",
                table: "SanctionCheck",
                column: "SubContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_BudgetOfficeId",
                schema: "staff",
                table: "Staff",
                column: "BudgetOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_LocationId",
                schema: "staff",
                table: "Staff",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_TaskId",
                schema: "staff",
                table: "Staff",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffSubContractor_SubContractorsId",
                table: "StaffSubContractor",
                column: "SubContractorsId");

            migrationBuilder.CreateIndex(
                name: "IX_SubContractor_AccountManagerId",
                schema: "subcontractors",
                table: "SubContractor",
                column: "AccountManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_SubContractor_LocationId",
                schema: "subcontractors",
                table: "SubContractor",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportingDocumentation_InvoiceId",
                schema: "invoice",
                table: "SupportingDocumentation",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_ParentTaskId",
                schema: "tasks",
                table: "Task",
                column: "ParentTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_ProjectId",
                schema: "tasks",
                table: "Task",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_ResponsiblePersonId",
                schema: "tasks",
                table: "Task",
                column: "ResponsiblePersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Tax_SubContractorId",
                schema: "tax",
                table: "Tax",
                column: "SubContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tax_TaxTypeId",
                schema: "tax",
                table: "Tax",
                column: "TaxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheet_ParentTimeSheetId",
                schema: "tasks",
                table: "TimeSheet",
                column: "ParentTimeSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheet_StaffId",
                schema: "tasks",
                table: "TimeSheet",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheet_TaskId",
                schema: "tasks",
                table: "TimeSheet",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addendum_Agreement_AgreementId",
                schema: "agreements",
                table: "Addendum",
                column: "AgreementId",
                principalSchema: "agreements",
                principalTable: "Agreement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddendumProject_Project_ProjectsId",
                table: "AddendumProject",
                column: "ProjectsId",
                principalSchema: "project",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddendumStaff_Staff_StaffsId",
                table: "AddendumStaff",
                column: "StaffsId",
                principalSchema: "staff",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_SubContractor_SubContractorId",
                schema: "agreements",
                table: "Agreement",
                column: "SubContractorId",
                principalSchema: "subcontractors",
                principalTable: "SubContractor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BackgroundCheck_Staff_ApproverId",
                schema: "checks",
                table: "BackgroundCheck",
                column: "ApproverId",
                principalSchema: "staff",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BackgroundCheck_Staff_StaffId",
                schema: "checks",
                table: "BackgroundCheck",
                column: "StaffId",
                principalSchema: "staff",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BackgroundCheck_SubContractor_SubContractorId",
                schema: "checks",
                table: "BackgroundCheck",
                column: "SubContractorId",
                principalSchema: "subcontractors",
                principalTable: "SubContractor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Compliance_SubContractor_SubContractorId",
                schema: "compliance",
                table: "Compliance",
                column: "SubContractorId",
                principalSchema: "subcontractors",
                principalTable: "SubContractor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Project_ProjectId",
                schema: "invoice",
                table: "Invoice",
                column: "ProjectId",
                principalSchema: "project",
                principalTable: "Project",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Staff_ReferralId",
                schema: "invoice",
                table: "Invoice",
                column: "ReferralId",
                principalSchema: "staff",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_SubContractor_SubContractorId",
                schema: "invoice",
                table: "Invoice",
                column: "SubContractorId",
                principalSchema: "subcontractors",
                principalTable: "SubContractor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_SupportingDocumentation_InvoiceFileId",
                schema: "invoice",
                table: "Invoice",
                column: "InvoiceFileId",
                principalSchema: "invoice",
                principalTable: "SupportingDocumentation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketSubContractor_SubContractor_SubContractorsId",
                schema: "subcontractors",
                table: "MarketSubContractor",
                column: "SubContractorsId",
                principalSchema: "subcontractors",
                principalTable: "SubContractor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Task_TaskId",
                schema: "tasks",
                table: "Note",
                column: "TaskId",
                principalSchema: "tasks",
                principalTable: "Task",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfficeSubContractor_SubContractor_SubContractorsId",
                schema: "subcontractors",
                table: "OfficeSubContractor",
                column: "SubContractorsId",
                principalSchema: "subcontractors",
                principalTable: "SubContractor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_ProjectGroup_ProjectGroupId",
                schema: "project",
                table: "Project",
                column: "ProjectGroupId",
                principalSchema: "project",
                principalTable: "ProjectGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Staff_DeliveryManagerId",
                schema: "project",
                table: "Project",
                column: "DeliveryManagerId",
                principalSchema: "staff",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Staff_InvoiceApproverId",
                schema: "project",
                table: "Project",
                column: "InvoiceApproverId",
                principalSchema: "staff",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Staff_ProjectManagerId",
                schema: "project",
                table: "Project",
                column: "ProjectManagerId",
                principalSchema: "staff",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectGroup_Staff_DeliveryManagerId",
                schema: "project",
                table: "ProjectGroup",
                column: "DeliveryManagerId",
                principalSchema: "staff",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectStaff_Staff_StaffsId",
                table: "ProjectStaff",
                column: "StaffsId",
                principalSchema: "staff",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSubContractor_SubContractor_SubContractorsId",
                table: "ProjectSubContractor",
                column: "SubContractorsId",
                principalSchema: "subcontractors",
                principalTable: "SubContractor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rate_Staff_StaffId",
                schema: "agreements",
                table: "Rate",
                column: "StaffId",
                principalSchema: "staff",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SanctionCheck_Staff_ApproverId",
                schema: "checks",
                table: "SanctionCheck",
                column: "ApproverId",
                principalSchema: "staff",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SanctionCheck_Staff_StaffId",
                schema: "checks",
                table: "SanctionCheck",
                column: "StaffId",
                principalSchema: "staff",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SanctionCheck_SubContractor_SubContractorId",
                schema: "checks",
                table: "SanctionCheck",
                column: "SubContractorId",
                principalSchema: "subcontractors",
                principalTable: "SubContractor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Task_TaskId",
                schema: "staff",
                table: "Staff",
                column: "TaskId",
                principalSchema: "tasks",
                principalTable: "Task",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addendum_Agreement_AgreementId",
                schema: "agreements",
                table: "Addendum");

            migrationBuilder.DropForeignKey(
                name: "FK_Addendum_Currency_CurrencyId",
                schema: "agreements",
                table: "Addendum");

            migrationBuilder.DropForeignKey(
                name: "FK_Addendum_PaymentTerm_PaymentTermId",
                schema: "agreements",
                table: "Addendum");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Addendum_AddendumId",
                schema: "invoice",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Project_ProjectId",
                schema: "invoice",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Project_ProjectId",
                schema: "tasks",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Staff_ReferralId",
                schema: "invoice",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_SubContractor_Staff_AccountManagerId",
                schema: "subcontractors",
                table: "SubContractor");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Staff_ResponsiblePersonId",
                schema: "tasks",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_SubContractor_Location_LocationId",
                schema: "subcontractors",
                table: "SubContractor");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_SubContractor_SubContractorId",
                schema: "invoice",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_BudgedInfo_BudgedInfoId",
                schema: "invoice",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Milestone_MilestoneId",
                schema: "invoice",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_SupportingDocumentation_InvoiceFileId",
                schema: "invoice",
                table: "Invoice");

            migrationBuilder.DropTable(
                name: "AddendumProject");

            migrationBuilder.DropTable(
                name: "AddendumStaff");

            migrationBuilder.DropTable(
                name: "BackgroundCheck",
                schema: "checks");

            migrationBuilder.DropTable(
                name: "ComplianceFile",
                schema: "compliance");

            migrationBuilder.DropTable(
                name: "MarketSubContractor",
                schema: "subcontractors");

            migrationBuilder.DropTable(
                name: "Note",
                schema: "tasks");

            migrationBuilder.DropTable(
                name: "OfficeSubContractor",
                schema: "subcontractors");

            migrationBuilder.DropTable(
                name: "ProjectStaff");

            migrationBuilder.DropTable(
                name: "ProjectSubContractor");

            migrationBuilder.DropTable(
                name: "Rate",
                schema: "agreements");

            migrationBuilder.DropTable(
                name: "SanctionCheck",
                schema: "checks");

            migrationBuilder.DropTable(
                name: "StaffSubContractor");

            migrationBuilder.DropTable(
                name: "Tax",
                schema: "tax");

            migrationBuilder.DropTable(
                name: "TimeSheet",
                schema: "tasks");

            migrationBuilder.DropTable(
                name: "Compliance",
                schema: "compliance");

            migrationBuilder.DropTable(
                name: "Market",
                schema: "subcontractors");

            migrationBuilder.DropTable(
                name: "Office",
                schema: "subcontractors");

            migrationBuilder.DropTable(
                name: "RateUnit",
                schema: "agreements");

            migrationBuilder.DropTable(
                name: "TaxType",
                schema: "tax");

            migrationBuilder.DropTable(
                name: "ComplianceRating",
                schema: "compliance");

            migrationBuilder.DropTable(
                name: "Agreement",
                schema: "agreements");

            migrationBuilder.DropTable(
                name: "LegalEntity",
                schema: "subcontractors");

            migrationBuilder.DropTable(
                name: "PaymentMethod",
                schema: "budget");

            migrationBuilder.DropTable(
                name: "Currency",
                schema: "common");

            migrationBuilder.DropTable(
                name: "PaymentTerm",
                schema: "budget");

            migrationBuilder.DropTable(
                name: "Addendum",
                schema: "agreements");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "project");

            migrationBuilder.DropTable(
                name: "BudgetGroup",
                schema: "budget");

            migrationBuilder.DropTable(
                name: "ProjectGroup",
                schema: "project");

            migrationBuilder.DropTable(
                name: "Staff",
                schema: "staff");

            migrationBuilder.DropTable(
                name: "Task",
                schema: "tasks");

            migrationBuilder.DropTable(
                name: "Location",
                schema: "common");

            migrationBuilder.DropTable(
                name: "SubContractor",
                schema: "subcontractors");

            migrationBuilder.DropTable(
                name: "BudgedInfo",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "Milestone",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "SupportingDocumentation",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "Invoice",
                schema: "invoice");
        }
    }
}
