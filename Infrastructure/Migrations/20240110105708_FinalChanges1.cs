using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class FinalChanges1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Mortgages",
                table: "Mortgages");

            migrationBuilder.DropColumn(
                name: "MortgageId",
                table: "Mortgages");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Mortgages",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mortgages",
                table: "Mortgages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mortgages_CustomerId",
                table: "Mortgages",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Mortgages_PropertyId",
                table: "Mortgages",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mortgages_Customers_CustomerId",
                table: "Mortgages",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mortgages_Properties_PropertyId",
                table: "Mortgages",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mortgages_Customers_CustomerId",
                table: "Mortgages");

            migrationBuilder.DropForeignKey(
                name: "FK_Mortgages_Properties_PropertyId",
                table: "Mortgages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mortgages",
                table: "Mortgages");

            migrationBuilder.DropIndex(
                name: "IX_Mortgages_CustomerId",
                table: "Mortgages");

            migrationBuilder.DropIndex(
                name: "IX_Mortgages_PropertyId",
                table: "Mortgages");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Mortgages");

            migrationBuilder.AddColumn<int>(
                name: "MortgageId",
                table: "Mortgages",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mortgages",
                table: "Mortgages",
                column: "MortgageId");
        }
    }
}
