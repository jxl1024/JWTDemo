using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JWTDemo.Server.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserCode = table.Column<string>(maxLength: 40, nullable: false),
                    UserName = table.Column<string>(maxLength: 40, nullable: false),
                    Password = table.Column<string>(maxLength: 40, nullable: false),
                    Sex = table.Column<int>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    CreateUserId = table.Column<string>(nullable: true),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    UpdateUserId = table.Column<string>(nullable: true),
                    UpdateDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Age", "CreateDateTime", "CreateUserId", "Password", "Sex", "UpdateDateTime", "UpdateUserId", "UserCode", "UserName" },
                values: new object[] { 1, 23, new DateTime(2019, 11, 27, 16, 52, 11, 352, DateTimeKind.Local), "System", "System", 1, new DateTime(2019, 11, 27, 16, 52, 11, 353, DateTimeKind.Local), "System", "System", "超级管理员" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Age", "CreateDateTime", "CreateUserId", "Password", "Sex", "UpdateDateTime", "UpdateUserId", "UserCode", "UserName" },
                values: new object[] { 2, 23, new DateTime(2019, 11, 27, 16, 52, 11, 354, DateTimeKind.Local), "System", "123456", 1, new DateTime(2019, 11, 27, 16, 52, 11, 354, DateTimeKind.Local), "System", "admin", "普通管理员" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Age", "CreateDateTime", "CreateUserId", "Password", "Sex", "UpdateDateTime", "UpdateUserId", "UserCode", "UserName" },
                values: new object[] { 3, 25, new DateTime(2019, 11, 27, 16, 52, 11, 354, DateTimeKind.Local), "System", "345923", 2, new DateTime(2019, 11, 27, 16, 52, 11, 354, DateTimeKind.Local), "System", "345923", "小红" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
