using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrator.Migrations
{
    /// <inheritdoc />
    public partial class CategoryHierarchy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ums",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("f566ff19-37be-4f82-8858-df3b540b502c"));

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                schema: "store",
                table: "Category",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentId",
                schema: "store",
                table: "Category",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentId",
                schema: "store",
                table: "Category",
                column: "ParentId",
                principalSchema: "store",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_ParentId",
                schema: "store",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_ParentId",
                schema: "store",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ParentId",
                schema: "store",
                table: "Category");

            migrationBuilder.InsertData(
                schema: "ums",
                table: "Role",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "Name", "NormalizedName", "Type" },
                values: new object[] { new Guid("f566ff19-37be-4f82-8858-df3b540b502c"), "SYSTEM_ADMIN_ROLE", null, "Admin Role", null, "Admin" });
        }
    }
}
