using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RocketMqtt.Migrations
{
    /// <inheritdoc />
    public partial class addUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false, comment: "主键"),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "用户名"),
                    Password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "用户名"),
                    Salt = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false, comment: "加密盐"),
                    Remark = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "备注")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
