using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrator.Migrations
{
    /// <inheritdoc />
    public partial class RemovePathFileEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullPath",
                table: "FileEntry");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "FileEntry");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullPath",
                table: "FileEntry",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "FileEntry",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
