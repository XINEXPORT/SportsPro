using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsPro.Migrations
{
    public partial class UpdateCustomerSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryID",
                table: "Incidents",
                type: "nvarchar(450)",
                nullable: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_CountryID",
                table: "Incidents",
                column: "CountryID"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Countries_CountryID",
                table: "Incidents",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Countries_CountryID",
                table: "Incidents"
            );

            migrationBuilder.DropIndex(name: "IX_Incidents_CountryID", table: "Incidents");

            migrationBuilder.DropColumn(name: "CountryID", table: "Incidents");
        }
    }
}
