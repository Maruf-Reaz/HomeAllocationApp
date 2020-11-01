using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BnHomeAllocationApp.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResidenceResidents");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "OfficerResidenceInfoes");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "OfficerResidenceInfoes",
                newName: "VacancyReason");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Residences",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Officers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PNO",
                table: "Officers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AllottementDate",
                table: "OfficerResidenceInfoes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ApplicationTypeId",
                table: "OfficerResidenceInfoes",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVancancyProbable",
                table: "OfficerResidenceInfoes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "JoiningDate",
                table: "OfficerResidenceInfoes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Preference",
                table: "OfficerResidenceInfoes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProbableVacancyDate",
                table: "OfficerResidenceInfoes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "OfficerResidenceInfoes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "OfficerResidenceInfoes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResidenceId",
                table: "OfficerResidenceInfoes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VacancyDate",
                table: "OfficerResidenceInfoes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ApplicationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ApplicationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "General" },
                    { 2, "Special Consideration" },
                    { 3, "NAM" }
                });

            migrationBuilder.InsertData(
                table: "BuildingTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "A Type" },
                    { 2, "B Type" },
                    { 3, "C Type" }
                });

            migrationBuilder.InsertData(
                table: "OfficerRanks",
                columns: new[] { "Id", "Name", "RankPoint" },
                values: new object[,]
                {
                    { 9, "Sub Lieutenant", 0 },
                    { 8, "Lieutenant", 0 },
                    { 7, "Lieutenant Commander", 3 },
                    { 6, "Commander", 5 },
                    { 3, "Rear Admiral", 15 },
                    { 4, "Commodore", 9 },
                    { 2, "Vice Admiral", 0 },
                    { 1, "Admiral", 0 },
                    { 5, "Captain", 7 }
                });

            migrationBuilder.InsertData(
                table: "ResidenceZones",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "ShaheenBag" },
                    { 1, "NHQ Residential Area" },
                    { 3, "Nakhal Para Dhaka" }
                });

            migrationBuilder.InsertData(
                table: "ResidenceBuildings",
                columns: new[] { "Id", "BuildingTypeId", "Name", "ResidenceZoneId" },
                values: new object[] { 1, 1, "Bungalow", 1 });

            migrationBuilder.InsertData(
                table: "ResidenceBuildings",
                columns: new[] { "Id", "BuildingTypeId", "Name", "ResidenceZoneId" },
                values: new object[] { 2, 2, "10 Storied Building No -108", 1 });

            migrationBuilder.InsertData(
                table: "ResidenceBuildings",
                columns: new[] { "Id", "BuildingTypeId", "Name", "ResidenceZoneId" },
                values: new object[] { 3, 2, "121 B-1", 2 });

            migrationBuilder.InsertData(
                table: "Residences",
                columns: new[] { "Id", "Details", "IsAllotted", "Name", "ResidenceBuildingId" },
                values: new object[,]
                {
                    { 1, null, false, "Bungalow A/1", 1 },
                    { 2, null, false, "Bungalow A/2", 1 },
                    { 3, null, false, "Bungalow A/3", 1 },
                    { 4, null, false, "108 B/1", 2 },
                    { 5, null, false, "108 B/2", 2 },
                    { 6, null, false, "108 B/3", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfficerResidenceInfoes_ApplicationTypeId",
                table: "OfficerResidenceInfoes",
                column: "ApplicationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficerResidenceInfoes_ResidenceId",
                table: "OfficerResidenceInfoes",
                column: "ResidenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfficerResidenceInfoes_ApplicationTypes_ApplicationTypeId",
                table: "OfficerResidenceInfoes",
                column: "ApplicationTypeId",
                principalTable: "ApplicationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficerResidenceInfoes_Residences_ResidenceId",
                table: "OfficerResidenceInfoes",
                column: "ResidenceId",
                principalTable: "Residences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfficerResidenceInfoes_ApplicationTypes_ApplicationTypeId",
                table: "OfficerResidenceInfoes");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficerResidenceInfoes_Residences_ResidenceId",
                table: "OfficerResidenceInfoes");

            migrationBuilder.DropTable(
                name: "ApplicationTypes");

            migrationBuilder.DropIndex(
                name: "IX_OfficerResidenceInfoes_ApplicationTypeId",
                table: "OfficerResidenceInfoes");

            migrationBuilder.DropIndex(
                name: "IX_OfficerResidenceInfoes_ResidenceId",
                table: "OfficerResidenceInfoes");

            migrationBuilder.DeleteData(
                table: "BuildingTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OfficerRanks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OfficerRanks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OfficerRanks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OfficerRanks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OfficerRanks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OfficerRanks",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OfficerRanks",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "OfficerRanks",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "OfficerRanks",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ResidenceBuildings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ResidenceZones",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Residences",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Residences",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Residences",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Residences",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Residences",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Residences",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ResidenceBuildings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ResidenceBuildings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ResidenceZones",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BuildingTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BuildingTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ResidenceZones",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Details",
                table: "Residences");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Officers");

            migrationBuilder.DropColumn(
                name: "PNO",
                table: "Officers");

            migrationBuilder.DropColumn(
                name: "AllottementDate",
                table: "OfficerResidenceInfoes");

            migrationBuilder.DropColumn(
                name: "ApplicationTypeId",
                table: "OfficerResidenceInfoes");

            migrationBuilder.DropColumn(
                name: "IsVancancyProbable",
                table: "OfficerResidenceInfoes");

            migrationBuilder.DropColumn(
                name: "JoiningDate",
                table: "OfficerResidenceInfoes");

            migrationBuilder.DropColumn(
                name: "Preference",
                table: "OfficerResidenceInfoes");

            migrationBuilder.DropColumn(
                name: "ProbableVacancyDate",
                table: "OfficerResidenceInfoes");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "OfficerResidenceInfoes");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "OfficerResidenceInfoes");

            migrationBuilder.DropColumn(
                name: "ResidenceId",
                table: "OfficerResidenceInfoes");

            migrationBuilder.DropColumn(
                name: "VacancyDate",
                table: "OfficerResidenceInfoes");

            migrationBuilder.RenameColumn(
                name: "VacancyReason",
                table: "OfficerResidenceInfoes",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "OfficerResidenceInfoes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ResidenceResidents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllottementDate = table.Column<DateTime>(nullable: false),
                    OfficerId = table.Column<int>(nullable: false),
                    ProbableVacancyDate = table.Column<DateTime>(nullable: false),
                    ResidenceId = table.Column<int>(nullable: false),
                    VacancyDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidenceResidents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResidenceResidents_Officers_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "Officers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResidenceResidents_Residences_ResidenceId",
                        column: x => x.ResidenceId,
                        principalTable: "Residences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResidenceResidents_OfficerId",
                table: "ResidenceResidents",
                column: "OfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_ResidenceResidents_ResidenceId",
                table: "ResidenceResidents",
                column: "ResidenceId");
        }
    }
}
