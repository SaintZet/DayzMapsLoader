using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DayzMapsLoader.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayzMaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayzMaps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MapProviders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProvidersMapAssets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MapProviderId = table.Column<int>(type: "int", nullable: false),
                    DayzMapId = table.Column<int>(type: "int", nullable: false),
                    MapName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MapType = table.Column<int>(type: "int", nullable: false),
                    MaxMapLevel = table.Column<int>(type: "int", nullable: false),
                    MapVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MapExtension = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvidersMapAssets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProvidersMapAssets_DayzMaps_DayzMapId",
                        column: x => x.DayzMapId,
                        principalTable: "DayzMaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProvidersMapAssets_MapProviders_MapProviderId",
                        column: x => x.MapProviderId,
                        principalTable: "MapProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProvidersMapAssets_DayzMapId",
                table: "ProvidersMapAssets",
                column: "DayzMapId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvidersMapAssets_MapProviderId",
                table: "ProvidersMapAssets",
                column: "MapProviderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProvidersMapAssets");

            migrationBuilder.DropTable(
                name: "DayzMaps");

            migrationBuilder.DropTable(
                name: "MapProviders");
        }
    }
}
