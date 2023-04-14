using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Add_Owned_Setting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "access_point",
                table: "device_info",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ap_password",
                table: "device_info",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "mqtt_UserName",
                table: "device_info",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "mqtt_password",
                table: "device_info",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "mqtt_port",
                table: "device_info",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "mqtt_server",
                table: "device_info",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "device_info",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "access_point",
                table: "device_info");

            migrationBuilder.DropColumn(
                name: "ap_password",
                table: "device_info");

            migrationBuilder.DropColumn(
                name: "mqtt_UserName",
                table: "device_info");

            migrationBuilder.DropColumn(
                name: "mqtt_password",
                table: "device_info");

            migrationBuilder.DropColumn(
                name: "mqtt_port",
                table: "device_info");

            migrationBuilder.DropColumn(
                name: "mqtt_server",
                table: "device_info");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "device_info");
        }
    }
}
