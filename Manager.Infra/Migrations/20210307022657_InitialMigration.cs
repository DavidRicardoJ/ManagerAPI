using Microsoft.EntityFrameworkCore.Migrations;

namespace Manager.Infra.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    email = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
