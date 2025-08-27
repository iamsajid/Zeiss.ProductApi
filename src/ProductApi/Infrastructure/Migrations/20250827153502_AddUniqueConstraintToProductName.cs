using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToProductName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AvailableStock", "Category", "CreatedAt", "Name", "Price" },
                values: new object[,]
                {
                    { 123456, 120, "Eyewear", new DateTime(2025, 8, 27, 15, 35, 1, 957, DateTimeKind.Utc).AddTicks(9250), "VisionPro Eyeglass Lens", 149.99m },
                    { 123457, 200, "Eyewear", new DateTime(2025, 8, 27, 15, 35, 1, 957, DateTimeKind.Utc).AddTicks(9250), "BlueLight Blocking Glasses", 89.50m },
                    { 123458, 15, "Medical", new DateTime(2025, 8, 27, 15, 35, 1, 957, DateTimeKind.Utc).AddTicks(9250), "Precision Microscope X200", 5499.00m },
                    { 123459, 25, "Medical", new DateTime(2025, 8, 27, 15, 35, 1, 957, DateTimeKind.Utc).AddTicks(9250), "Portable Diagnostic Kit", 1299.00m },
                    { 123460, 40, "Electronics", new DateTime(2025, 8, 27, 15, 35, 1, 957, DateTimeKind.Utc).AddTicks(9250), "CinePrime 50mm Camera Lens", 899.50m },
                    { 123461, 12, "Electronics", new DateTime(2025, 8, 27, 15, 35, 1, 957, DateTimeKind.Utc).AddTicks(9250), "Compact Projector StarView", 799.00m },
                    { 123462, 60, "Electronics", new DateTime(2025, 8, 27, 15, 35, 1, 957, DateTimeKind.Utc).AddTicks(9260), "VR Headset VisionX", 499.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Name",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 123456);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 123457);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 123458);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 123459);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 123460);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 123461);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 123462);
        }
    }
}
