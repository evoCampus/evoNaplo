using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evoNaplo.Migrations
{
    /// <inheritdoc />
    public partial class FixStudentImportFieldNullabilityAndTeamTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ScholarshipDuration",
                table: "Students",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "WorkingStudentDuration",
                table: "Students",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.Sql("UPDATE [Students] SET [ScholarshipDuration] = NULL WHERE [ScholarshipDuration] = '0001-01-01T00:00:00.0000000'");
            migrationBuilder.Sql("UPDATE [Students] SET [WorkingStudentDuration] = NULL WHERE [WorkingStudentDuration] = '0001-01-01T00:00:00.0000000'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [Students] SET [ScholarshipDuration] = '0001-01-01T00:00:00.0000000' WHERE [ScholarshipDuration] IS NULL");
            migrationBuilder.Sql("UPDATE [Students] SET [WorkingStudentDuration] = '0001-01-01T00:00:00.0000000' WHERE [WorkingStudentDuration] IS NULL");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScholarshipDuration",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "WorkingStudentDuration",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
