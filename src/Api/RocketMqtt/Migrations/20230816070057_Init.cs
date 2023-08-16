using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RocketMqtt.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConnInfo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false, comment: "主键"),
                    ClientId = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "客户端Id"),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "客户端用户名"),
                    Endpoint = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "地址"),
                    KeepAlive = table.Column<uint>(type: "INTEGER", nullable: false, comment: "心跳（秒）"),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false, comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscribed",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false, comment: "主键"),
                    ClientId = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "客户端Id"),
                    TopicName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "主题名称"),
                    Qos = table.Column<int>(type: "INTEGER", nullable: false, comment: "Qos")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribed", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false, comment: "主键"),
                    TopicName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "主题名称"),
                    Node = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "节点")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnInfo");

            migrationBuilder.DropTable(
                name: "Subscribed");

            migrationBuilder.DropTable(
                name: "Topic");
        }
    }
}
