using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "device_info",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    uri = table.Column<string>(type: "TEXT", nullable: false),
                    limsi = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_device_info", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "operation_log",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    operation_time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    flash_in_state = table.Column<bool>(type: "INTEGER", nullable: true),
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operation_log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_operation_log_device_info_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "device_info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_device_info_limsi",
                table: "device_info",
                column: "limsi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_device_info_uri",
                table: "device_info",
                column: "uri",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_operation_log_DeviceId",
                table: "operation_log",
                column: "DeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "operation_log");

            migrationBuilder.DropTable(
                name: "device_info");
        }
    }
}