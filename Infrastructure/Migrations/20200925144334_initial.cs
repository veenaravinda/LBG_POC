using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false, maxLength: 50),
                    Email = table.Column<string>(nullable: false, maxLength: 50),
                    Phone = table.Column<string>(nullable: false, maxLength: 15),
                    Income = table.Column<string>(nullable: false),

                    // Income = table.Column<decimal>(nullable: false, type: "decimal(18,2)"),
                    CreditScore = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });
            // migrationBuilder.Sql("ALTER TABLE Customers ADD CONSTRAINT CHK_PositiveDecimal CHECK (Income > 0)");


            migrationBuilder.CreateTable(
               name: "Properties",
               columns: table => new
               {
                   Id = table.Column<int>(nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   Address = table.Column<string>(nullable: false, maxLength: 50),
                   City = table.Column<string>(nullable: false, maxLength: 50),
                   State = table.Column<string>(nullable: false, maxLength: 15),
                   ZipCode = table.Column<string>(nullable: false),
                   MarketValue = table.Column<decimal>(nullable: false, type: "decimal(18,2)"),
                   PropType = table.Column<string>(nullable: false),
                   BedRooms = table.Column<int>(nullable: false),
                   Bathroom = table.Column<int>(nullable: false),
                   YearBuilt = table.Column<int>(nullable: false),
                   Size = table.Column<int>(nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Property", x => x.Id);
               });

            migrationBuilder.CreateTable(
              name: "Mortgages",
              columns: table => new
              {
                  Id = table.Column<int>(nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  CustomerId = table.Column<int>(nullable: false),
                  PropertyId = table.Column<int>(nullable: false),
                  LoanDuration = table.Column<int>(nullable: false),
                  TotalIntrest = table.Column<decimal>(nullable: false, type: "decimal(18,2)"),
                  MonthlyPayment = table.Column<decimal>(nullable: false, type: "decimal(18,2)"),
                  CreatedAt = table.Column<DateTime>(nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Mortgages", x => x.Id);
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
            // migrationBuilder.Sql("ALTER TABLE Customers ADD CONSTRAINT CHK_PositiveDecimal CHECK (Income > 0)");
        }
                       
            
    protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Customers");
            migrationBuilder.DropTable(
               name: "Properties");
            migrationBuilder.DropTable(
               name: "Mortgages");
        }
    }
}
