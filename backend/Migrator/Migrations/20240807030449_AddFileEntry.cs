using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrator.Migrations
{
    /// <inheritdoc />
    public partial class AddFileEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FileEntryCollectionId",
                schema: "store",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "FileEntryCollection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileEntryCollection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileEntryCollectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileEntry_FileEntryCollection_FileEntryCollectionId",
                        column: x => x.FileEntryCollectionId,
                        principalTable: "FileEntryCollection",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_FileEntryCollectionId",
                schema: "store",
                table: "Product",
                column: "FileEntryCollectionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileEntry_FileEntryCollectionId",
                table: "FileEntry",
                column: "FileEntryCollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_FileEntryCollection_FileEntryCollectionId",
                schema: "store",
                table: "Product",
                column: "FileEntryCollectionId",
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
                name: "FileEntry");

            migrationBuilder.DropTable(
                name: "FileEntryCollection");

            migrationBuilder.DropIndex(
                name: "IX_Product_FileEntryCollectionId",
                schema: "store",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "FileEntryCollectionId",
                schema: "store",
                table: "Product");
        }
    }
}
