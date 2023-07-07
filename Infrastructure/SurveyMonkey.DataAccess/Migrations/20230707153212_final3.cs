using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyMonkey.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class final3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineAnswers_Questions_QuestionId",
                table: "LineAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_MultiChoiceAnswers_Choices_ChoiceId",
                table: "MultiChoiceAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_MultiChoiceAnswers_Questions_QuestionId",
                table: "MultiChoiceAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceAnswers_Choices_ChoiceId",
                table: "SingleChoiceAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceAnswers_Questions_QuestionId",
                table: "SingleChoiceAnswers");

            migrationBuilder.AddForeignKey(
                name: "FK_LineAnswers_Questions_QuestionId",
                table: "LineAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MultiChoiceAnswers_Choices_ChoiceId",
                table: "MultiChoiceAnswers",
                column: "ChoiceId",
                principalTable: "Choices",
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
                name: "FK_SingleChoiceAnswers_Choices_ChoiceId",
                table: "SingleChoiceAnswers",
                column: "ChoiceId",
                principalTable: "Choices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_LineAnswers_Questions_QuestionId",
                table: "LineAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_MultiChoiceAnswers_Choices_ChoiceId",
                table: "MultiChoiceAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_MultiChoiceAnswers_Questions_QuestionId",
                table: "MultiChoiceAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceAnswers_Choices_ChoiceId",
                table: "SingleChoiceAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceAnswers_Questions_QuestionId",
                table: "SingleChoiceAnswers");

            migrationBuilder.AddForeignKey(
                name: "FK_LineAnswers_Questions_QuestionId",
                table: "LineAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MultiChoiceAnswers_Choices_ChoiceId",
                table: "MultiChoiceAnswers",
                column: "ChoiceId",
                principalTable: "Choices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MultiChoiceAnswers_Questions_QuestionId",
                table: "MultiChoiceAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChoiceAnswers_Choices_ChoiceId",
                table: "SingleChoiceAnswers",
                column: "ChoiceId",
                principalTable: "Choices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChoiceAnswers_Questions_QuestionId",
                table: "SingleChoiceAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");
        }
    }
}
