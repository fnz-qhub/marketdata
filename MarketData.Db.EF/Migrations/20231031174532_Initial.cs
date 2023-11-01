using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketData.Db.EF.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Funds",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Isin = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Class = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Funds", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Prices",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                FundId = table.Column<long>(type: "bigint", nullable: false),
                Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                Price = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                PriceDate = table.Column<DateOnly>(type: "date", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Prices", x => x.Id);
                table.ForeignKey(
                    name: "FK_Prices_Funds_FundId",
                    column: x => x.FundId,
                    principalTable: "Funds",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Funds_Isin",
            table: "Funds",
            column: "Isin",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Prices_FundId",
            table: "Prices",
            column: "FundId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Prices");

        migrationBuilder.DropTable(
            name: "Funds");
    }
}
