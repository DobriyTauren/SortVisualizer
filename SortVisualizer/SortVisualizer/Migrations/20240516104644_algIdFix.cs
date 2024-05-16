using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SortVisualizer.Migrations
{
    /// <inheritdoc />
    public partial class algIdFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Algorithms_AlgorithmId",
                table: "Histories");

            migrationBuilder.DropIndex(
                name: "IX_Histories_AlgorithmId",
                table: "Histories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Histories_AlgorithmId",
                table: "Histories",
                column: "AlgorithmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Algorithms_AlgorithmId",
                table: "Histories",
                column: "AlgorithmId",
                principalTable: "Algorithms",
                principalColumn: "Id");
        }
    }
}
