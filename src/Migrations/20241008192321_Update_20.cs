using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_2.Migrations
{
    /// <inheritdoc />
    public partial class Update_20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donmua_product_Productid",
                schema: "Data",
                table: "Donmua");

            migrationBuilder.DropIndex(
                name: "IX_Donmua_Productid",
                schema: "Data",
                table: "Donmua");

            migrationBuilder.DropColumn(
                name: "Productid",
                schema: "Data",
                table: "Donmua");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Productid",
                schema: "Data",
                table: "Donmua",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Donmua_Productid",
                schema: "Data",
                table: "Donmua",
                column: "Productid");

            migrationBuilder.AddForeignKey(
                name: "FK_Donmua_product_Productid",
                schema: "Data",
                table: "Donmua",
                column: "Productid",
                principalSchema: "Data",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
