using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_2.Migrations
{
    /// <inheritdoc />
    public partial class Update_18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "nguoiban",
                schema: "Data",
                table: "ThanhToan",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "nguoimua",
                schema: "Data",
                table: "ThanhToan",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "dongia",
                schema: "Data",
                table: "Donmua",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "Data",
                table: "Donmua",
                type: "text",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddColumn<int>(
                name: "soluong",
                schema: "Data",
                table: "Donmua",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "tongtien",
                schema: "Data",
                table: "Donmua",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nguoiban",
                schema: "Data",
                table: "ThanhToan");

            migrationBuilder.DropColumn(
                name: "nguoimua",
                schema: "Data",
                table: "ThanhToan");

            migrationBuilder.DropColumn(
                name: "dongia",
                schema: "Data",
                table: "Donmua");

            migrationBuilder.DropColumn(
                name: "name",
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

            migrationBuilder.DropColumn(
                name: "soluong",
                schema: "Data",
                table: "Donmua");

            migrationBuilder.DropColumn(
                name: "tongtien",
                schema: "Data",
                table: "Donmua");
        }
    }
}
