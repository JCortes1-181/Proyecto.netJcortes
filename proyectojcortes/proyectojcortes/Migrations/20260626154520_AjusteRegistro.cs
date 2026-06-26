using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectojcortes.Migrations
{
    /// <inheritdoc />
    public partial class AjusteRegistro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registros_Criaturas_CriaturaId",
                table: "Registros");

            migrationBuilder.AlterColumn<int>(
                name: "CriaturaId",
                table: "Registros",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Registros_Criaturas_CriaturaId",
                table: "Registros",
                column: "CriaturaId",
                principalTable: "Criaturas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registros_Criaturas_CriaturaId",
                table: "Registros");

            migrationBuilder.AlterColumn<int>(
                name: "CriaturaId",
                table: "Registros",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Registros_Criaturas_CriaturaId",
                table: "Registros",
                column: "CriaturaId",
                principalTable: "Criaturas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
