using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyMonkey.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class final2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultiChoiceAnswers_Answers_AnswerId",
                table: "MultiChoiceAnswers");

            migrationBuilder.AddForeignKey(
                name: "FK_MultiChoiceAnswers_Answers_AnswerId",
                table: "MultiChoiceAnswers",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultiChoiceAnswers_Answers_AnswerId",
                table: "MultiChoiceAnswers");

            migrationBuilder.AddForeignKey(
                name: "FK_MultiChoiceAnswers_Answers_AnswerId",
                table: "MultiChoiceAnswers",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id");
        }
    }
}
