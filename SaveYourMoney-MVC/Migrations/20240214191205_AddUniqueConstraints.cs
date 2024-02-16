using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaveYourMoney_MVC.Migrations
{
    public partial class AddUniqueConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "Unique_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Unique_Customers_Username",
                table: "Customers",
                column: "Username",
                unique: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
