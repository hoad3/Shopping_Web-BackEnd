using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_2.Migrations
{
    /// <inheritdoc />
    public partial class Update_25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_delivery_ThanhToan_ThanhtoanId",
                schema: "Data",
                table: "delivery");

            migrationBuilder.DropForeignKey(
                name: "FK_delivery_ThanhToan_thanhtoanid",
                schema: "Data",
                table: "delivery");

            migrationBuilder.DropForeignKey(
                name: "FK_delivery_shipper_idshipper",
                schema: "Data",
                table: "delivery");

            migrationBuilder.DropForeignKey(
                name: "FK_delivery_shipper_shipperidshipper",
                schema: "Data",
                table: "delivery");

            migrationBuilder.DropIndex(
                name: "IX_delivery_shipperidshipper",
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

            migrationBuilder.DropColumn(
                name: "shipperidshipper",
                schema: "Data",
                table: "delivery");

            migrationBuilder.AddForeignKey(
                name: "FK_delivery_ThanhToan_thanhtoanid",
                schema: "Data",
                table: "delivery",
                column: "thanhtoanid",
                principalSchema: "Data",
                principalTable: "ThanhToan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_delivery_shipper_idshipper",
                schema: "Data",
                table: "delivery",
                column: "idshipper",
                principalSchema: "Data",
                principalTable: "shipper",
                principalColumn: "idshipper",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_delivery_ThanhToan_thanhtoanid",
                schema: "Data",
                table: "delivery");

            migrationBuilder.DropForeignKey(
                name: "FK_delivery_shipper_idshipper",
                schema: "Data",
                table: "delivery");

            migrationBuilder.AddColumn<int>(
                name: "ThanhtoanId",
                schema: "Data",
                table: "delivery",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "shipperidshipper",
                schema: "Data",
                table: "delivery",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_delivery_shipperidshipper",
                schema: "Data",
                table: "delivery",
                column: "shipperidshipper");

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

            migrationBuilder.AddForeignKey(
                name: "FK_delivery_ThanhToan_thanhtoanid",
                schema: "Data",
                table: "delivery",
                column: "thanhtoanid",
                principalSchema: "Data",
                principalTable: "ThanhToan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_delivery_shipper_idshipper",
                schema: "Data",
                table: "delivery",
                column: "idshipper",
                principalSchema: "Data",
                principalTable: "shipper",
                principalColumn: "idshipper",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_delivery_shipper_shipperidshipper",
                schema: "Data",
                table: "delivery",
                column: "shipperidshipper",
                principalSchema: "Data",
                principalTable: "shipper",
                principalColumn: "idshipper",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
