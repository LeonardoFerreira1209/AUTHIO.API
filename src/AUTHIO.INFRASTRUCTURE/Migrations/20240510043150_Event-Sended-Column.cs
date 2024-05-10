using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AUTHIO.INFRASTRUCTURE.Migrations
{
    /// <inheritdoc />
    public partial class EventSendedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("278d2723-54f5-43b3-b145-eaff88575613"));

            migrationBuilder.AddColumn<DateTime>(
                name: "Sended",
                table: "Events",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoleId",
                value: new Guid("063553c5-f359-4e0e-94e7-23e6f54d5fd7"));

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "RoleId",
                value: new Guid("063553c5-f359-4e0e-94e7-23e6f54d5fd7"));

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "RoleId",
                value: new Guid("063553c5-f359-4e0e-94e7-23e6f54d5fd7"));

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4,
                column: "RoleId",
                value: new Guid("063553c5-f359-4e0e-94e7-23e6f54d5fd7"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Created", "Name", "NormalizedName", "Status", "System", "TenantId", "Updated" },
                values: new object[] { new Guid("063553c5-f359-4e0e-94e7-23e6f54d5fd7"), null, new DateTime(2024, 5, 10, 1, 31, 50, 355, DateTimeKind.Local).AddTicks(4511), "System", "SYSTEM", 1, true, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("063553c5-f359-4e0e-94e7-23e6f54d5fd7"));

            migrationBuilder.DropColumn(
                name: "Sended",
                table: "Events");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoleId",
                value: new Guid("278d2723-54f5-43b3-b145-eaff88575613"));

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "RoleId",
                value: new Guid("278d2723-54f5-43b3-b145-eaff88575613"));

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "RoleId",
                value: new Guid("278d2723-54f5-43b3-b145-eaff88575613"));

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4,
                column: "RoleId",
                value: new Guid("278d2723-54f5-43b3-b145-eaff88575613"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Created", "Name", "NormalizedName", "Status", "System", "TenantId", "Updated" },
                values: new object[] { new Guid("278d2723-54f5-43b3-b145-eaff88575613"), null, new DateTime(2024, 5, 8, 0, 22, 55, 299, DateTimeKind.Local).AddTicks(5798), "System", "SYSTEM", 1, true, null, null });
        }
    }
}
