using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Column_Typo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_operation_log_device_info_DeviceId",
                table: "operation_log");

            migrationBuilder.DropPrimaryKey(
                name: "PK_operation_log",
                table: "operation_log");

            migrationBuilder.DropPrimaryKey(
                name: "PK_device_info",
                table: "device_info");

            migrationBuilder.RenameTable(
                name: "operation_log",
                newName: "OperationLogs");

            migrationBuilder.RenameTable(
                name: "device_info",
                newName: "Devices");

            migrationBuilder.RenameColumn(
                name: "operation_time",
                table: "OperationLogs",
                newName: "OperationTime");

            migrationBuilder.RenameColumn(
                name: "flash_in_state",
                table: "OperationLogs",
                newName: "FlashInState");

            migrationBuilder.RenameIndex(
                name: "IX_operation_log_DeviceId",
                table: "OperationLogs",
                newName: "IX_OperationLogs_DeviceId");

            migrationBuilder.RenameColumn(
                name: "uri",
                table: "Devices",
                newName: "Uri");

            migrationBuilder.RenameColumn(
                name: "limsi",
                table: "Devices",
                newName: "Limsi");

            migrationBuilder.RenameColumn(
                name: "mqtt_server",
                table: "Devices",
                newName: "Settings_MqttServer");

            migrationBuilder.RenameColumn(
                name: "mqtt_port",
                table: "Devices",
                newName: "Settings_MqttPort");

            migrationBuilder.RenameColumn(
                name: "mqtt_password",
                table: "Devices",
                newName: "Settings_MqttPassword");

            migrationBuilder.RenameColumn(
                name: "mqtt_UserName",
                table: "Devices",
                newName: "Settings_MqttUserName");

            migrationBuilder.RenameColumn(
                name: "ap_password",
                table: "Devices",
                newName: "Settings_ApPassword");

            migrationBuilder.RenameColumn(
                name: "access_point",
                table: "Devices",
                newName: "Settings_AccessPoint");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Devices",
                newName: "Settings_ApUserId");

            migrationBuilder.RenameIndex(
                name: "IX_device_info_uri",
                table: "Devices",
                newName: "IX_Devices_Uri");

            migrationBuilder.RenameIndex(
                name: "IX_device_info_limsi",
                table: "Devices",
                newName: "IX_Devices_Limsi");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OperationLogs",
                table: "OperationLogs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Devices",
                table: "Devices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationLogs_Devices_DeviceId",
                table: "OperationLogs",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationLogs_Devices_DeviceId",
                table: "OperationLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OperationLogs",
                table: "OperationLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Devices",
                table: "Devices");

            migrationBuilder.RenameTable(
                name: "OperationLogs",
                newName: "operation_log");

            migrationBuilder.RenameTable(
                name: "Devices",
                newName: "device_info");

            migrationBuilder.RenameColumn(
                name: "OperationTime",
                table: "operation_log",
                newName: "operation_time");

            migrationBuilder.RenameColumn(
                name: "FlashInState",
                table: "operation_log",
                newName: "flash_in_state");

            migrationBuilder.RenameIndex(
                name: "IX_OperationLogs_DeviceId",
                table: "operation_log",
                newName: "IX_operation_log_DeviceId");

            migrationBuilder.RenameColumn(
                name: "Uri",
                table: "device_info",
                newName: "uri");

            migrationBuilder.RenameColumn(
                name: "Limsi",
                table: "device_info",
                newName: "limsi");

            migrationBuilder.RenameColumn(
                name: "Settings_MqttUserName",
                table: "device_info",
                newName: "mqtt_UserName");

            migrationBuilder.RenameColumn(
                name: "Settings_MqttServer",
                table: "device_info",
                newName: "mqtt_server");

            migrationBuilder.RenameColumn(
                name: "Settings_MqttPort",
                table: "device_info",
                newName: "mqtt_port");

            migrationBuilder.RenameColumn(
                name: "Settings_MqttPassword",
                table: "device_info",
                newName: "mqtt_password");

            migrationBuilder.RenameColumn(
                name: "Settings_ApPassword",
                table: "device_info",
                newName: "ap_password");

            migrationBuilder.RenameColumn(
                name: "Settings_AccessPoint",
                table: "device_info",
                newName: "access_point");

            migrationBuilder.RenameColumn(
                name: "Settings_ApUserId",
                table: "device_info",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Devices_Uri",
                table: "device_info",
                newName: "IX_device_info_uri");

            migrationBuilder.RenameIndex(
                name: "IX_Devices_Limsi",
                table: "device_info",
                newName: "IX_device_info_limsi");

            migrationBuilder.AddPrimaryKey(
                name: "PK_operation_log",
                table: "operation_log",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_device_info",
                table: "device_info",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_operation_log_device_info_DeviceId",
                table: "operation_log",
                column: "DeviceId",
                principalTable: "device_info",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
