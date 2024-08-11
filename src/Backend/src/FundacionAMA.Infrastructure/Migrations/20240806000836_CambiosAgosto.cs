using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FundacionAMA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CambiosAgosto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Brigades_Name",
                table: "Brigades");

            migrationBuilder.UpdateData(
                table: "ActivityType",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 5, 19, 8, 25, 77, DateTimeKind.Local).AddTicks(9799));

            migrationBuilder.UpdateData(
                table: "DonationType",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 5, 19, 8, 25, 91, DateTimeKind.Local).AddTicks(9678));

            migrationBuilder.UpdateData(
                table: "DonationType",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 5, 19, 8, 25, 91, DateTimeKind.Local).AddTicks(9701));

            migrationBuilder.UpdateData(
                table: "DonationType",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 5, 19, 8, 25, 91, DateTimeKind.Local).AddTicks(9703));

            migrationBuilder.UpdateData(
                table: "DonationType",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 5, 19, 8, 25, 91, DateTimeKind.Local).AddTicks(9706));

            migrationBuilder.UpdateData(
                table: "DonationType",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 5, 19, 8, 25, 91, DateTimeKind.Local).AddTicks(9708));

            migrationBuilder.UpdateData(
                table: "DonationType",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 5, 19, 8, 25, 91, DateTimeKind.Local).AddTicks(9710));

            migrationBuilder.UpdateData(
                table: "DonationType",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 5, 19, 8, 25, 91, DateTimeKind.Local).AddTicks(9712));

            migrationBuilder.UpdateData(
                table: "IdentificationType",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 5, 19, 8, 25, 97, DateTimeKind.Local).AddTicks(9614));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "TempCode" },
                values: new object[] { new DateTime(2024, 8, 5, 19, 8, 25, 106, DateTimeKind.Local).AddTicks(2611), "be70c7f2-0a04-45c4-830c-27f024bfccca" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ActivityType",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 4, 16, 31, 31, 684, DateTimeKind.Local).AddTicks(2166));

            migrationBuilder.UpdateData(
                table: "DonationType",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 4, 16, 31, 31, 688, DateTimeKind.Local).AddTicks(8673));

            migrationBuilder.UpdateData(
                table: "DonationType",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 4, 16, 31, 31, 688, DateTimeKind.Local).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "DonationType",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 4, 16, 31, 31, 688, DateTimeKind.Local).AddTicks(8690));

            migrationBuilder.UpdateData(
                table: "DonationType",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 4, 16, 31, 31, 688, DateTimeKind.Local).AddTicks(8691));

            migrationBuilder.UpdateData(
                table: "DonationType",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 4, 16, 31, 31, 688, DateTimeKind.Local).AddTicks(8692));

            migrationBuilder.UpdateData(
                table: "DonationType",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 4, 16, 31, 31, 688, DateTimeKind.Local).AddTicks(8694));

            migrationBuilder.UpdateData(
                table: "DonationType",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 4, 16, 31, 31, 688, DateTimeKind.Local).AddTicks(8695));

            migrationBuilder.UpdateData(
                table: "IdentificationType",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 4, 16, 31, 31, 689, DateTimeKind.Local).AddTicks(7062));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "TempCode" },
                values: new object[] { new DateTime(2024, 8, 4, 16, 31, 31, 691, DateTimeKind.Local).AddTicks(2740), "52a9e02e-cc7e-41a0-8909-9887beef59aa" });

            
        }
    }
}
