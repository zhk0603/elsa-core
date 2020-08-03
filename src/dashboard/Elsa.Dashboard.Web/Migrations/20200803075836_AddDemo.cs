using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Elsa.Dashboard.Web.Migrations
{
    public partial class AddDemo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Approves",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    WfInstanceId = table.Column<int>(nullable: false),
                    WfActivityInstanceId = table.Column<int>(nullable: false),
                    AuditStatus = table.Column<byte>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    CorrelateId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approves", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Days = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ApproveId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRecords_Approves_ApproveId",
                        column: x => x.ApproveId,
                        principalTable: "Approves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRecords_ApproveId",
                table: "LeaveRecords",
                column: "ApproveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRecords");

            migrationBuilder.DropTable(
                name: "Approves");
        }
    }
}
