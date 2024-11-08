using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_2.Migrations
{
    /// <inheritdoc />
    public partial class Update_24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThanhtoanId",
                schema: "Data",
                table: "delivery",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_delivery_ThanhtoanId",
                schema: "Data",
                table: "delivery",
                column: "ThanhtoanId");

            migrationBuilder.AddForeignKey(
                name: "FK_delivery_ThanhToan_ThanhtoanId",
                schema: "Data",
                table: "delivery",
                column: "ThanhtoanId",
                principalSchema: "Data",
                principalTable: "ThanhToan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_delivery_ThanhToan_ThanhtoanId",
                schema: "Data",
                table: "delivery");

            migrationBuilder.DropIndex(
                name: "IX_delivery_ThanhtoanId",
                schema: "Data",
                table: "delivery");

            migrationBuilder.DropColumn(
                name: "ThanhtoanId",
                schema: "Data",
                table: "delivery");
        }
    }
}
