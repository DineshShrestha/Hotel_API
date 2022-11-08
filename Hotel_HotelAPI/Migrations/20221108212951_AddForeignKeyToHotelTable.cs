using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_HotelAPI.Migrations
{
    public partial class AddForeignKeyToHotelTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HotelID",
                table: "HotelNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 8, 22, 29, 51, 561, DateTimeKind.Local).AddTicks(4396));

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 8, 22, 29, 51, 561, DateTimeKind.Local).AddTicks(4869));

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 8, 22, 29, 51, 561, DateTimeKind.Local).AddTicks(4872));

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 8, 22, 29, 51, 561, DateTimeKind.Local).AddTicks(4876));

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 8, 22, 29, 51, 561, DateTimeKind.Local).AddTicks(4878));

            migrationBuilder.CreateIndex(
                name: "IX_HotelNumbers_HotelID",
                table: "HotelNumbers",
                column: "HotelID");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelNumbers_Hotels_HotelID",
                table: "HotelNumbers",
                column: "HotelID",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelNumbers_Hotels_HotelID",
                table: "HotelNumbers");

            migrationBuilder.DropIndex(
                name: "IX_HotelNumbers_HotelID",
                table: "HotelNumbers");

            migrationBuilder.DropColumn(
                name: "HotelID",
                table: "HotelNumbers");

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 8, 21, 52, 32, 224, DateTimeKind.Local).AddTicks(4302));

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 8, 21, 52, 32, 224, DateTimeKind.Local).AddTicks(4352));

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 8, 21, 52, 32, 224, DateTimeKind.Local).AddTicks(4354));

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 8, 21, 52, 32, 224, DateTimeKind.Local).AddTicks(4356));

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 8, 21, 52, 32, 224, DateTimeKind.Local).AddTicks(4358));
        }
    }
}
