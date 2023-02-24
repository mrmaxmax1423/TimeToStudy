using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeToStudy.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(nullable: false),
                    ClassStartTime = table.Column<string>(nullable: false),
                    ClassLength = table.Column<double>(nullable: false),
                    CreditHours = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassId);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventLabel = table.Column<string>(nullable: false),
                    EventDescription = table.Column<string>(nullable: false),
                    EventLength = table.Column<double>(nullable: false),
                    Reccuring = table.Column<bool>(nullable: false),
                    SetTime = table.Column<bool>(nullable: false),
                    EventTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "ClassId", "ClassLength", "ClassName", "ClassStartTime", "CreditHours" },
                values: new object[,]
                {
                    { 1, 1.0, "Calculus II", "11:00", 3.0 },
                    { 2, 2.0, "Chemistry", "12:30", 3.0 }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "EventDescription", "EventLabel", "EventLength", "EventTime", "Reccuring", "SetTime" },
                values: new object[,]
                {
                    { 1, "1", "2", 3.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2023), false, false },
                    { 2, "3", "4", 5.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2023), true, false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
