using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SortVisualizer.Migrations
{
    /// <inheritdoc />
    public partial class addVisType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VisualizationType",
                table: "Histories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisualizationType",
                table: "Histories");
        }
    }
}
