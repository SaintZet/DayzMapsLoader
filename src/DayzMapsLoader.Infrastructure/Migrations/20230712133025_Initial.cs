using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DayzMapsLoader.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MapProviders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlQueryTemplate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "date", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MapTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProvidedMaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameForProvider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MapProviderId = table.Column<int>(type: "int", nullable: false),
                    MapId = table.Column<int>(type: "int", nullable: false),
                    MapTypeId = table.Column<int>(type: "int", nullable: false),
                    MapTypeForProvider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxMapLevel = table.Column<int>(type: "int", nullable: false),
                    IsFirstQuadrant = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageExtension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvidedMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProvidedMaps_MapProviders_MapProviderId",
                        column: x => x.MapProviderId,
                        principalTable: "MapProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProvidedMaps_MapTypes_MapTypeId",
                        column: x => x.MapTypeId,
                        principalTable: "MapTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProvidedMaps_Maps_MapId",
                        column: x => x.MapId,
                        principalTable: "Maps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MapProviders",
                columns: new[] { "Id", "Link", "Name", "UrlQueryTemplate" },
                values: new object[,]
                {
                    { 1, "https://xam.nu/", "Xam", "https://static.xam.nu/dayz/maps/{Map.NameForProvider}/{Map.Version}/{Map.MapTypeForProvider}/{Zoom}/{X}/{Y}.{Map.ImageExtension}" },
                    { 2, "https://ginfo.gg/", "GInfo", "https://maps.izurvive.com/maps/{Map.NameForProvider}-{Map.MapTypeForProvider}/{Map.Version}/tiles/{Zoom}/{X}/{Y}.{Map.ImageExtension}" }
                });

            migrationBuilder.InsertData(
                table: "MapTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "topographic" },
                    { 2, "satellite" }
                });

            migrationBuilder.InsertData(
                table: "Maps",
                columns: new[] { "Id", "Author", "LastUpdate", "Link", "Name" },
                values: new object[,]
                {
                    { 1, "Bohemia Interactive", new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.bohemia.net", "Chernarus" },
                    { 2, "Bohemia Interactive", new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.bohemia.net", "Livonia" },
                    { 3, "Sumrak", new DateTime(2022, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://steamcommunity.com/workshop/filedetails/?id=2289456201", "Namalsk" },
                    { 4, "RonhillUltra", new DateTime(2021, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://steamcommunity.com/sharedfiles/filedetails/?id=2462896799", "Esseker" },
                    { 5, "CypeRevenge", new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://steamcommunity.com/sharedfiles/filedetails/?id=2563233742", "Takistan" },
                    { 6, "KubeloLive", new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://steamcommunity.com/sharedfiles/filedetails/?id=2415195639", "Banov" }
                });

            migrationBuilder.InsertData(
                table: "ProvidedMaps",
                columns: new[] { "Id", "ImageExtension", "IsFirstQuadrant", "MapId", "MapProviderId", "MapTypeForProvider", "MapTypeId", "MaxMapLevel", "NameForProvider", "Version" },
                values: new object[,]
                {
                    { 1, "jpg", false, 1, 1, "topographic", 1, 7, "chernarusplus", "1.17-1" },
                    { 2, "jpg", false, 1, 1, "satellite", 2, 7, "chernarusplus", "1.17-1" },
                    { 3, "webp", false, 2, 1, "topographic", 1, 7, "livonia", "1.21" },
                    { 4, "webp", false, 2, 1, "satellite", 2, 7, "livonia", "1.21" },
                    { 5, "jpg", false, 3, 1, "topographic", 1, 7, "namalsk", "CE3" },
                    { 6, "jpg", false, 3, 1, "satellite", 2, 7, "namalsk", "CE3" },
                    { 7, "jpg", false, 4, 1, "topographic", 1, 7, "esseker", "0.58" },
                    { 8, "jpg", false, 4, 1, "satellite", 2, 7, "esseker", "0.58" },
                    { 9, "jpg", false, 5, 1, "topographic", 1, 7, "takistanplus", "1.041" },
                    { 10, "jpg", false, 5, 1, "satellite", 2, 7, "takistanplus", "1.041" },
                    { 11, "webp", false, 6, 1, "topographic", 1, 7, "banov", "Feb.21" },
                    { 12, "webp", false, 6, 1, "satellite", 2, 7, "banov", "Feb.21" },
                    { 13, "webp", false, 1, 2, "Top", 1, 8, "ChernarusPlus", "1.0.0" },
                    { 14, "webp", false, 1, 2, "Sat", 2, 8, "ChernarusPlus", "1.0.0" },
                    { 15, "webp", false, 2, 2, "Top", 1, 8, "Livonia", "1.19.0" },
                    { 16, "webp", false, 2, 2, "Sat", 2, 8, "Livonia", "1.19.0" },
                    { 17, "webp", true, 3, 2, "Top", 1, 7, "Namalsk", "1.2.0" },
                    { 18, "webp", true, 3, 2, "Sat", 2, 7, "Namalsk", "1.2.0" },
                    { 19, "png", true, 4, 2, "Top", 1, 7, "Esseker", "1.1.0" },
                    { 20, "png", true, 4, 2, "Sat", 2, 7, "Esseker", "1.1.0" },
                    { 21, "webp", true, 5, 2, "Top", 1, 7, "TakistanPlus", "1.2.0" },
                    { 22, "webp", true, 5, 2, "Sat", 2, 7, "TakistanPlus", "1.2.0" },
                    { 23, "webp", false, 6, 2, "Top", 1, 7, "Banov", "1.6.0" },
                    { 24, "webp", false, 6, 2, "Sat", 2, 7, "Banov", "1.6.0" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProvidedMaps_MapId",
                table: "ProvidedMaps",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvidedMaps_MapProviderId",
                table: "ProvidedMaps",
                column: "MapProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvidedMaps_MapTypeId",
                table: "ProvidedMaps",
                column: "MapTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProvidedMaps");

            migrationBuilder.DropTable(
                name: "MapProviders");

            migrationBuilder.DropTable(
                name: "MapTypes");

            migrationBuilder.DropTable(
                name: "Maps");
        }
    }
}
