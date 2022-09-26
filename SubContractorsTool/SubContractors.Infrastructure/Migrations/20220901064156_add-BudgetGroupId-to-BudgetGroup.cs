using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubContractors.Infrastructure.Migrations
{
    public partial class addBudgetGroupIdtoBudgetGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                schema: "budget",
                table: "BudgetGroup",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.AddColumn<int>(
                name: "BudgetSystemId",
                schema: "budget",
                table: "BudgetGroup",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BudgetSystemId",
                schema: "budget",
                table: "BudgetGroup");

            migrationBuilder.InsertData(
                schema: "budget",
                table: "BudgetGroup",
                columns: new[] { "Id", "BudgetGroupName", "EmailAddress", "IsActive", "IsDefault", "IsDeleted", "IsFunction", "ParentBudgetGroupId" },
                values: new object[] { 32, "DataArt", "", false, false, false, false, null });

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
        }
    }
}
