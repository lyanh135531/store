using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrator.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnFileEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Size",
                table: "FileEntry",
                newName: "Length");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "FileEntry",
                newName: "FileName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Length",
                table: "FileEntry",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "FileEntry",
                newName: "Name");
        }
    }
}
