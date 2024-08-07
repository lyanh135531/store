using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrator.Migrations
{
    /// <inheritdoc />
    public partial class AddFileEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedTime",
                schema: "store",
                table: "Product",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<Guid>(
                name: "FileEntryCollectionId",
                schema: "store",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedTime",
                schema: "store",
                table: "Order",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "FileEntryCollection",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileEntryCollection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileEntry",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileEntryCollectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileEntry_FileEntryCollection_FileEntryCollectionId",
                        column: x => x.FileEntryCollectionId,
                        principalSchema: "dbo",
                        principalTable: "FileEntryCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_FileEntryCollectionId",
                schema: "store",
                table: "Product",
                column: "FileEntryCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_FileEntry_FileEntryCollectionId",
                schema: "dbo",
                table: "FileEntry",
                column: "FileEntryCollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_FileEntryCollection_FileEntryCollectionId",
                schema: "store",
                table: "Product",
                column: "FileEntryCollectionId",
                principalSchema: "dbo",
                principalTable: "FileEntryCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_FileEntryCollection_FileEntryCollectionId",
                schema: "store",
                table: "Product");

            migrationBuilder.DropTable(
                name: "FileEntry",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FileEntryCollection",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Product_FileEntryCollectionId",
                schema: "store",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "FileEntryCollectionId",
                schema: "store",
                table: "Product");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedTime",
                schema: "store",
                table: "Product",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedTime",
                schema: "store",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
