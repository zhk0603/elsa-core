using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Elsa.Persistence.EntityFrameworkCore.Migrations.SqlServer
{
    public partial class AddExecutionActivityEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ActivityInstances",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "ActivityInstances",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ActivityInstances",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ActivityDefinitions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "ActivityDefinitions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ActivityDefinitions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExecutionActivities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkflowInstanceId = table.Column<int>(nullable: true),
                    ActivityId = table.Column<string>(nullable: true),
                    ActivityType = table.Column<string>(nullable: true),
                    StartedAt = table.Column<DateTime>(nullable: false),
                    FinishedAt = table.Column<DateTime>(nullable: true),
                    FaultedAt = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    HandleStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutionActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExecutionActivities_WorkflowInstances_WorkflowInstanceId",
                        column: x => x.WorkflowInstanceId,
                        principalTable: "WorkflowInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExecutionActivities_WorkflowInstanceId",
                table: "ExecutionActivities",
                column: "WorkflowInstanceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExecutionActivities");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ActivityInstances");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "ActivityInstances");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ActivityInstances");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ActivityDefinitions");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "ActivityDefinitions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ActivityDefinitions");
        }
    }
}
