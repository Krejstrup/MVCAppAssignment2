using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCAppAssignment2.Migrations
{
    public partial class RemovedIdentityDoublets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CityId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Peoples_Cities_PeopleId",
                table: "Peoples");

            migrationBuilder.DropIndex(
                name: "IX_Peoples_PeopleId",
                table: "Peoples");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CityId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "PeopleId",
                table: "Peoples");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "PeopleId",
                table: "Cities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeopleId",
                table: "Peoples",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Countries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Cities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PeopleId",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Peoples_PeopleId",
                table: "Peoples",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CityId",
                table: "Cities",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CityId",
                table: "Cities",
                column: "CityId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Peoples_Cities_PeopleId",
                table: "Peoples",
                column: "PeopleId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
