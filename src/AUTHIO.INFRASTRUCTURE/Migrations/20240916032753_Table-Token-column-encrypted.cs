using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AUTHIO.INFRASTRUCTURE.Migrations
{
    /// <inheritdoc />
    public partial class TableTokencolumnencrypted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Encrypted",
                table: "TenantTokenConfigurations",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Encrypted",
                table: "TenantTokenConfigurations");
        }
    }
}
