using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaveYourMoney_MVC.Migrations
{
    public partial class RenameCustomerIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BudgetId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "CusomterId",
                table: "Categories",
                newName: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CustomerId",
                table: "Categories",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_CategoryId",
                table: "Budgets",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_CustomerId",
                table: "Budgets",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Categories_CategoryId",
                table: "Budgets",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Customers_CustomerId",
                table: "Budgets",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Customers_CustomerId",
                table: "Categories",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Categories_CategoryId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Customers_CustomerId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Customers_CustomerId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CustomerId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_CategoryId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_CustomerId",
                table: "Budgets");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Categories",
                newName: "CusomterId");

            migrationBuilder.AddColumn<int>(
                name: "BudgetId",
                table: "Categories",
                type: "INTEGER",
                nullable: true);
        }
    }
}
