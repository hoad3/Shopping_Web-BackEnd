using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_2.Migrations
{
    /// <inheritdoc />
    public partial class Update_19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donmua_InformationUser_idnguoiban",
                schema: "Data",
                table: "Donmua");

            migrationBuilder.DropForeignKey(
                name: "FK_Donmua_InformationUser_idnguoimua",
                schema: "Data",
                table: "Donmua");

            migrationBuilder.AddColumn<int>(
                name: "Productid",
                schema: "Data",
                table: "Donmua",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "nguoiban",
                schema: "Data",
                table: "Donmua",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "nguoimua",
                schema: "Data",
                table: "Donmua",
                type: "text",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "nguoiban",
                schema: "Data",
                table: "Donmua");

            migrationBuilder.DropColumn(
                name: "nguoimua",
                schema: "Data",
                table: "Donmua");

            migrationBuilder.AddForeignKey(
                name: "FK_Donmua_InformationUser_idnguoiban",
                schema: "Data",
                table: "Donmua",
                column: "idnguoiban",
                principalSchema: "Data",
                principalTable: "InformationUser",
                principalColumn: "Idname",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donmua_InformationUser_idnguoimua",
                schema: "Data",
                table: "Donmua",
                column: "idnguoimua",
                principalSchema: "Data",
                principalTable: "InformationUser",
                principalColumn: "Idname",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
