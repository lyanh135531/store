using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrator.Migrations
{
    /// <inheritdoc />
    public partial class UserDefaultValueSql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                schema: "ums",
                table: "User",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                schema: "ums",
                table: "User",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETDATE()",
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
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 3, 13, 48, 42, 268, DateTimeKind.Local).AddTicks(1946));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                schema: "ums",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

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
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "ums",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 3, 13, 48, 42, 268, DateTimeKind.Local).AddTicks(1946),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
