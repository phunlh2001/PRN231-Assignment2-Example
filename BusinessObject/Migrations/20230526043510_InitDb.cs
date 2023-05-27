using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", maxLength: 10, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    fullname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    gender = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    birthday = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "id", "address", "birthday", "fullname", "gender", "password", "username" },
                values: new object[] { 1, "Vietnam", "04/02/2001", "Kaiz Nguyenn", 0, "123456", "kaiznguyenn" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "id", "address", "birthday", "fullname", "gender", "password", "username" },
                values: new object[] { 2, "Germany", "19/10/2000", "Annie", 1, "654321", "annie12" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
