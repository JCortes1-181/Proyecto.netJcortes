using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectojcortes.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteToAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Registros",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Criaturas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Criaturas");
        }
    }
}
