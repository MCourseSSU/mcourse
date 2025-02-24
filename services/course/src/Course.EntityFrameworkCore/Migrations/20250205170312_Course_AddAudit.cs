using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class Course_AddAudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                schema: "course",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                schema: "course",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                schema: "course",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "course",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                schema: "course",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                schema: "course",
                table: "Courses");
        }
    }
}
