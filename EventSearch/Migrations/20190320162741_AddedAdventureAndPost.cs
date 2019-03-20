using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventSearch.Migrations
{
    public partial class AddedAdventureAndPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdventuresPost",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PostTitle = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdventuresPost", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_AdventuresPost_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "FirstName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdventuresTable",
                columns: table => new
                {
                    AdventureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    AdventurePostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdventuresTable", x => x.AdventureId);
                    table.ForeignKey(
                        name: "FK_AdventuresTable_AdventuresPost_AdventurePostId",
                        column: x => x.AdventurePostId,
                        principalTable: "AdventuresPost",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdventuresPost_UserId",
                table: "AdventuresPost",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdventuresTable_AdventurePostId",
                table: "AdventuresTable",
                column: "AdventurePostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdventuresTable");

            migrationBuilder.DropTable(
                name: "AdventuresPost");
        }
    }
}
