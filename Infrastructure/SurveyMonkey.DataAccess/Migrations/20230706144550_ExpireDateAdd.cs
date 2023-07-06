using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyMonkey.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ExpireDateAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                table: "Surveys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Surveys",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Surveys");
        }
    }
}
