using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubContractors.Infrastructure.Migrations
{
    public partial class mdpparameterssubcontractor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                schema: "subcontractors",
                table: "SubContractor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                schema: "subcontractors",
                table: "SubContractor",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                schema: "subcontractors",
                table: "SubContractor");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                schema: "subcontractors",
                table: "SubContractor");
        }
    }
}
