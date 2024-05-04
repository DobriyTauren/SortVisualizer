using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SortVisualizer.Migrations
{
    /// <inheritdoc />
    public partial class eeee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Algorithms_AlgorithmId",
                table: "Histories");

            migrationBuilder.AlterColumn<int>(
                name: "AlgorithmId",
                table: "Histories",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Algorithms_AlgorithmId",
                table: "Histories",
                column: "AlgorithmId",
                principalTable: "Algorithms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Algorithms_AlgorithmId",
                table: "Histories");

            migrationBuilder.AlterColumn<int>(
                name: "AlgorithmId",
                table: "Histories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Algorithms_AlgorithmId",
                table: "Histories",
                column: "AlgorithmId",
                principalTable: "Algorithms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
