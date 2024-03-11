using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaveYourMoney_MVC.Migrations
{
    public partial class addedCustomerIdtobudgetentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "Budgets",
            columns: table => new
          {
              BudgetId = table.Column<int>(nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
              CustomerId = table.Column<int>(nullable: false),
              CategoryId = table.Column<int>(nullable: false),
              Amount = table.Column<double>(nullable: false),
              Date = table.Column<DateTime>(nullable: false),
              Description = table.Column<string>(nullable: true)
          },
          constraints: table =>
          {
              table.PrimaryKey("PK_Budgets", x => x.BudgetId);
              table.ForeignKey(
                  name: "FK_Budgets_Customers_CustomerId",
                  column: x => x.CustomerId,
                  principalTable: "Customers",
                  principalColumn: "CustomerId",
                  onDelete: ReferentialAction.Cascade);
              table.ForeignKey(
                  name: "FK_Budgets_Categories_CategoryId",
                  column: x => x.CategoryId,
                  principalTable: "Categories",
                  principalColumn: "CategoryId",
                  onDelete: ReferentialAction.Cascade);
          });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "Budgets");
        }
    }
}
