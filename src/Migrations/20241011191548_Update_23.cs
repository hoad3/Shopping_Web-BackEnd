using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Web_2.Migrations
{
    /// <inheritdoc />
    public partial class Update_23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Trangthaithanhtoan",
                schema: "Data",
                table: "ThanhToan",
                newName: "trangthaidonhang");

            migrationBuilder.CreateTable(
                name: "shipper",
                schema: "Data",
                columns: table => new
                {
                    idshipper = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    shippername = table.Column<string>(type: "text", nullable: false),
                    shipperphone = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipper", x => x.idshipper);
                });

            migrationBuilder.CreateTable(
                name: "delivery",
                schema: "Data",
                columns: table => new
                {
                    deliveryid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idshipper = table.Column<int>(type: "integer", nullable: false),
                    thanhtoanid = table.Column<int>(type: "integer", nullable: false),
                    pickuptime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deliverytime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deliverystatus = table.Column<int>(type: "integer", nullable: false),
                    shipperidshipper = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery", x => x.deliveryid);
                    table.ForeignKey(
                        name: "FK_delivery_ThanhToan_thanhtoanid",
                        column: x => x.thanhtoanid,
                        principalSchema: "Data",
                        principalTable: "ThanhToan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_delivery_shipper_idshipper",
                        column: x => x.idshipper,
                        principalSchema: "Data",
                        principalTable: "shipper",
                        principalColumn: "idshipper",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_delivery_shipper_shipperidshipper",
                        column: x => x.shipperidshipper,
                        principalSchema: "Data",
                        principalTable: "shipper",
                        principalColumn: "idshipper",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_delivery_idshipper",
                schema: "Data",
                table: "delivery",
                column: "idshipper");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_shipperidshipper",
                schema: "Data",
                table: "delivery",
                column: "shipperidshipper");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_thanhtoanid",
                schema: "Data",
                table: "delivery",
                column: "thanhtoanid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "delivery",
                schema: "Data");

            migrationBuilder.DropTable(
                name: "shipper",
                schema: "Data");

            migrationBuilder.RenameColumn(
                name: "trangthaidonhang",
                schema: "Data",
                table: "ThanhToan",
                newName: "Trangthaithanhtoan");
        }
    }
}
