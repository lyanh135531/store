using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrator.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUmsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "ums",
                table: "User",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "ums",
                table: "User",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                schema: "ums",
                table: "User",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 7, 3, 13, 48, 42, 268, DateTimeKind.Local).AddTicks(1509),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 6, 26, 15, 41, 17, 553, DateTimeKind.Local).AddTicks(1659));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ums",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 3, 13, 48, 42, 268, DateTimeKind.Local).AddTicks(1946),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ums",
                table: "Role",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "ums",
                table: "Role",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                schema: "ums",
                table: "Role",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "Name", "NormalizedName", "Type" },
                values: new object[] { new Guid("f566ff19-37be-4f82-8858-df3b540b502c"), "SYSTEM_ADMIN_ROLE", null, "Admin Role", null, "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                schema: "ums",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                schema: "ums",
                table: "User",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_Code",
                schema: "ums",
                table: "Role",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Email",
                schema: "ums",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UserName",
                schema: "ums",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Role_Code",
                schema: "ums",
                table: "Role");

            migrationBuilder.DeleteData(
                schema: "ums",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("f566ff19-37be-4f82-8858-df3b540b502c"));

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "ums",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "ums",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                schema: "ums",
                table: "User",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 6, 26, 15, 41, 17, 553, DateTimeKind.Local).AddTicks(1659),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 7, 3, 13, 48, 42, 268, DateTimeKind.Local).AddTicks(1509));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ums",
                table: "User",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 3, 13, 48, 42, 268, DateTimeKind.Local).AddTicks(1946));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ums",
                table: "Role",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "ums",
                table: "Role",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
