using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeToStudy.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    EventLabel = table.Column<string>(nullable: false),
                    EventDescription = table.Column<string>(nullable: false),
                    EventLength = table.Column<double>(nullable: false),
                    Reccuring = table.Column<bool>(nullable: false),
                    SetTime = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "EventDescription", "EventLabel", "EventLength", "Reccuring", "SetTime" },
                values: new object[] { "001", "1", "2", 3.0, false, false });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "EventDescription", "EventLabel", "EventLength", "Reccuring", "SetTime" },
                values: new object[] { "002", "3", "4", 5.0, true, false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
