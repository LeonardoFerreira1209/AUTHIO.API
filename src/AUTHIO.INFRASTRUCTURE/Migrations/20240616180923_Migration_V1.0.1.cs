using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AUTHIO.INFRASTRUCTURE.Migrations
{
    /// <inheritdoc />
    public partial class Migration_V101 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "TenantTokenConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    SecurityKey = table.Column<string>(type: "longtext", nullable: true),
                    Issuer = table.Column<string>(type: "longtext", nullable: true),
                    Audience = table.Column<string>(type: "longtext", nullable: true),
                    TenantConfigurationId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantTokenConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantTokenConfigurations_TenantConfigurations_TenantConfigu~",
                        column: x => x.TenantConfigurationId,
                        principalTable: "TenantConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenantTokenConfigurations");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Created", "Name", "NormalizedName", "Status", "System", "TenantId", "Updated" },
                values: new object[] { new Guid("5c91cc7e-8e42-40d1-84bb-9351e5f07ab6"), null, new DateTime(2024, 6, 16, 13, 31, 10, 690, DateTimeKind.Local).AddTicks(8618), "System", "SYSTEM", 1, true, null, null });
        }
    }
}
