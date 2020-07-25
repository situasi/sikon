using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SiKon.Infrastructure.Persistence.Migrations
{
    public partial class SiKonDB_001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberID);
                });

            migrationBuilder.CreateTable(
                name: "TCPEndpoints",
                columns: table => new
                {
                    TCPEndpointID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    MemberID = table.Column<int>(nullable: true),
                    FriendlyName = table.Column<string>(nullable: true),
                    TargetAddress = table.Column<string>(nullable: true),
                    PortNumber = table.Column<int>(nullable: false),
                    CommandString = table.Column<string>(nullable: true),
                    SuccessString = table.Column<string>(nullable: true),
                    ErrorString = table.Column<string>(nullable: true),
                    CheckIntervalInMinutes = table.Column<int>(nullable: false),
                    RequestTimeOutInSeconds = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCPEndpoints", x => x.TCPEndpointID);
                    table.ForeignKey(
                        name: "FK_TCPEndpoints_Members_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TCPEndpoints_MemberID",
                table: "TCPEndpoints",
                column: "MemberID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TCPEndpoints");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
