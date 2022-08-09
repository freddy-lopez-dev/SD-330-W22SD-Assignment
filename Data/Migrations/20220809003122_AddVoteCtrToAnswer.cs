using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SD_330_W22SD_Assignment.Data.Migrations
{
    public partial class AddVoteCtrToAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VoteCtr",
                table: "Answer",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoteCtr",
                table: "Answer");
        }
    }
}
