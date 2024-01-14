using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Mortgage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mortgages",
                columns: table => new
                {
                    MortgageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    PropertyId = table.Column<int>(nullable: false),
                    LoanDuration = table.Column<int>(nullable: false),
                    LoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalIntrest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mortgages", x => x.MortgageId);
                    table.ForeignKey(
                          name: "FK_Mortgage_Customers",
                          column: x => x.CustomerId,
                          principalTable: "Customers",  // Name of the parent table
                          principalColumn: "PK_Customers", // Name of the primary key column in the parent table
                          onDelete: ReferentialAction.Cascade); // Define the desired behavior on delete
                    table.ForeignKey(
                            name: "FK_Mortgage_Properties",
                            column: x => x.PropertyId,
                            principalTable: "Properties",  // Name of the parent table
                            principalColumn: "PK_Property", // Name of the primary key column in the parent table
                            onDelete: ReferentialAction.Cascade); // Define the desired behavior on delete

                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mortgages");
        }
    }
}
