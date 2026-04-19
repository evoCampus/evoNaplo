using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evoNaplo.Migrations
{
    /// <inheritdoc />
    public partial class AttendanceSheetMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectLink_Projects_ProjectId",
                table: "ProjectLink");

            migrationBuilder.DropTable(
                name: "MentorProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectLink",
                table: "ProjectLink");

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "LengthOfMeeting",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "WeeklyMeetingTime",
                table: "Teams");

            migrationBuilder.RenameTable(
                name: "ProjectLink",
                newName: "ProjectLinks");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectLink_ProjectId",
                table: "ProjectLinks",
                newName: "IX_ProjectLinks_ProjectId");

            migrationBuilder.AddColumn<string>(
                name: "AttendanceSheetId",
                table: "Students",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniversityName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MentorId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjecCount",
                table: "Mentors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentCount",
                table: "Mentors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamCount",
                table: "Mentors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectLinks",
                table: "ProjectLinks",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AttendanceSheets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WeeklyMeetingTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LengthOfMeeting = table.Column<TimeSpan>(type: "time", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceSheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceSheets_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_AttendanceSheetId",
                table: "Students",
                column: "AttendanceSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_MentorId",
                table: "Projects",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSheets_TeamId",
                table: "AttendanceSheets",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectLinks_Projects_ProjectId",
                table: "ProjectLinks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Mentors_MentorId",
                table: "Projects",
                column: "MentorId",
                principalTable: "Mentors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AttendanceSheets_AttendanceSheetId",
                table: "Students",
                column: "AttendanceSheetId",
                principalTable: "AttendanceSheets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectLinks_Projects_ProjectId",
                table: "ProjectLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Mentors_MentorId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AttendanceSheets_AttendanceSheetId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "AttendanceSheets");

            migrationBuilder.DropIndex(
                name: "IX_Students_AttendanceSheetId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Projects_MentorId",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectLinks",
                table: "ProjectLinks");

            migrationBuilder.DropColumn(
                name: "AttendanceSheetId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UniversityName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "MentorId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjecCount",
                table: "Mentors");

            migrationBuilder.DropColumn(
                name: "StudentCount",
                table: "Mentors");

            migrationBuilder.DropColumn(
                name: "TeamCount",
                table: "Mentors");

            migrationBuilder.RenameTable(
                name: "ProjectLinks",
                newName: "ProjectLink");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectLinks_ProjectId",
                table: "ProjectLink",
                newName: "IX_ProjectLink_ProjectId");

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "LengthOfMeeting",
                table: "Teams",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "WeeklyMeetingTime",
                table: "Teams",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectLink",
                table: "ProjectLink",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MentorProject",
                columns: table => new
                {
                    MentorsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MentorProject", x => new { x.MentorsId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_MentorProject_Mentors_MentorsId",
                        column: x => x.MentorsId,
                        principalTable: "Mentors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MentorProject_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MentorProject_ProjectsId",
                table: "MentorProject",
                column: "ProjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectLink_Projects_ProjectId",
                table: "ProjectLink",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
