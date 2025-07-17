using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApi_Dotnet.Migrations
{
    /// <inheritdoc />
    public partial class SeederUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "Email", "IsActive", "Name", "Password", "Role", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { 1, new DateTime(2025, 7, 17, 6, 4, 25, 286, DateTimeKind.Utc).AddTicks(5222), null, null, "Admin@gmail.com", true, "", "Password", 0, null, null, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
