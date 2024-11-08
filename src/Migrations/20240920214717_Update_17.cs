using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Web_2.Migrations
{
    /// <inheritdoc />
    public partial class Update_17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Donmua",
                schema: "Data",
                columns: table => new
                {
                    Iddonmua = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idnguoiban = table.Column<int>(type: "integer", nullable: false),
                    idnguoimua = table.Column<int>(type: "integer", nullable: false),
                    idproduct = table.Column<int>(type: "integer", nullable: false),
                    ngaydat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donmua", x => x.Iddonmua);
                    table.ForeignKey(
                        name: "FK_Donmua_USER_idnguoiban",
                        column: x => x.idnguoiban,
                        principalSchema: "Data",
                        principalTable: "USER",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donmua_USER_idnguoimua",
                        column: x => x.idnguoimua,
                        principalSchema: "Data",
                        principalTable: "USER",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donmua_product_idproduct",
                        column: x => x.idproduct,
                        principalSchema: "Data",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donmua_idnguoiban",
                schema: "Data",
                table: "Donmua",
                column: "idnguoiban");

            migrationBuilder.CreateIndex(
                name: "IX_Donmua_idnguoimua",
                schema: "Data",
                table: "Donmua",
                column: "idnguoimua");

            migrationBuilder.CreateIndex(
                name: "IX_Donmua_idproduct",
                schema: "Data",
                table: "Donmua",
                column: "idproduct");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donmua",
                schema: "Data");
        }
    }
}
