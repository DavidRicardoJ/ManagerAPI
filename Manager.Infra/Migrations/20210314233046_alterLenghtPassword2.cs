using Microsoft.EntityFrameworkCore.Migrations;

namespace Manager.Infra.Migrations
{
    public partial class alterLenghtPassword2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "User",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(30)",
                oldMaxLength: 30);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "User",
                type: "VARCHAR(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);
        }
    }
}
