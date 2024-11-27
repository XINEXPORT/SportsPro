using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsPro.Migrations
{
    public partial class AddRegistrationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Customers_CustomerId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Products_ProductId",
                table: "Registrations");

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumns: new[] { "CustomerId", "ProductId" },
                keyValues: new object[] { 1002, 2 });

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumns: new[] { "CustomerId", "ProductId" },
                keyValues: new object[] { 1004, 3 });

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Registrations",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Registrations",
                newName: "CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_ProductId",
                table: "Registrations",
                newName: "IX_Registrations_ProductID");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 1015,
                column: "Email",
                value: "gonzalo@keeton.com");

            migrationBuilder.UpdateData(
                table: "Incidents",
                keyColumn: "IncidentID",
                keyValue: 1,
                column: "CustomerID",
                value: 1010);

            migrationBuilder.InsertData(
                table: "Registrations",
                columns: new[] { "CustomerID", "ProductID" },
                values: new object[,]
                {
                    { 1002, 3 },
                    { 1010, 2 }
                });

            migrationBuilder.UpdateData(
                table: "Technicians",
                keyColumn: "TechnicianID",
                keyValue: -1,
                column: "Name",
                value: "Assign a technician...");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Customers_CustomerID",
                table: "Registrations",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Products_ProductID",
                table: "Registrations",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Customers_CustomerID",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Products_ProductID",
                table: "Registrations");

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumns: new[] { "CustomerID", "ProductID" },
                keyValues: new object[] { 1002, 3 });

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumns: new[] { "CustomerID", "ProductID" },
                keyValues: new object[] { 1010, 2 });

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Registrations",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "Registrations",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_ProductID",
                table: "Registrations",
                newName: "IX_Registrations_ProductId");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 1015,
                column: "Email",
                value: "");

            migrationBuilder.UpdateData(
                table: "Incidents",
                keyColumn: "IncidentID",
                keyValue: 1,
                column: "CustomerID",
                value: 1002);

            migrationBuilder.InsertData(
                table: "Registrations",
                columns: new[] { "CustomerId", "ProductId" },
                values: new object[,]
                {
                    { 1002, 2 },
                    { 1004, 3 }
                });

            migrationBuilder.UpdateData(
                table: "Technicians",
                keyColumn: "TechnicianID",
                keyValue: -1,
                column: "Name",
                value: "Not assigned");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Customers_CustomerId",
                table: "Registrations",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Products_ProductId",
                table: "Registrations",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
