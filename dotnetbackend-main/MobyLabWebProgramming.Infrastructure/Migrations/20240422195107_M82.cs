using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobyLabWebProgramming.Infrastructure.Migrations
{
    public partial class M82 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Genre",
                type: "character varying(4095)",
                maxLength: 4095,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(4095)",
                oldMaxLength: 4095);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Genre",
                type: "character varying(4095)",
                maxLength: 4095,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(4095)",
                oldMaxLength: 4095,
                oldNullable: true);
        }
    }
}
