using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyMonkey.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineAnswers_Answers_AnswerId",
                table: "LineAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_LineAnswers_Questions_QuestionId",
                table: "LineAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_MultiChoiceAnswers_Questions_QuestionId",
                table: "MultiChoiceAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceAnswers_Answers_AnswerId",
                table: "SingleChoiceAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceAnswers_Questions_QuestionId",
                table: "SingleChoiceAnswers");

            migrationBuilder.AddForeignKey(
                name: "FK_LineAnswers_Answers_AnswerId",
                table: "LineAnswers",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LineAnswers_Questions_QuestionId",
                table: "LineAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MultiChoiceAnswers_Questions_QuestionId",
                table: "MultiChoiceAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChoiceAnswers_Answers_AnswerId",
                table: "SingleChoiceAnswers",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChoiceAnswers_Questions_QuestionId",
                table: "SingleChoiceAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineAnswers_Answers_AnswerId",
                table: "LineAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_LineAnswers_Questions_QuestionId",
                table: "LineAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_MultiChoiceAnswers_Questions_QuestionId",
                table: "MultiChoiceAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceAnswers_Answers_AnswerId",
                table: "SingleChoiceAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceAnswers_Questions_QuestionId",
                table: "SingleChoiceAnswers");

            migrationBuilder.AddForeignKey(
                name: "FK_LineAnswers_Answers_AnswerId",
                table: "LineAnswers",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LineAnswers_Questions_QuestionId",
                table: "LineAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MultiChoiceAnswers_Questions_QuestionId",
                table: "MultiChoiceAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChoiceAnswers_Answers_AnswerId",
                table: "SingleChoiceAnswers",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChoiceAnswers_Questions_QuestionId",
                table: "SingleChoiceAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");
        }
    }
}
