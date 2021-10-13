using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TILE03.Data.Migrations
{
    public partial class updateOefeningMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oefening_Antwoord_AntwoordId",
                table: "Oefening");

            migrationBuilder.DropForeignKey(
                name: "FK_Oefening_GroepsBewerking_GroepsBewerkingId",
                table: "Oefening");

            migrationBuilder.DropForeignKey(
                name: "FK_Opdrachten_Oefening_OefeningId",
                table: "Opdrachten");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Oefening",
                table: "Oefening");

            migrationBuilder.RenameTable(
                name: "Oefening",
                newName: "Oefeningen");

            migrationBuilder.RenameIndex(
                name: "IX_Oefening_GroepsBewerkingId",
                table: "Oefeningen",
                newName: "IX_Oefeningen_GroepsBewerkingId");

            migrationBuilder.RenameIndex(
                name: "IX_Oefening_AntwoordId",
                table: "Oefeningen",
                newName: "IX_Oefeningen_AntwoordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Oefeningen",
                table: "Oefeningen",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Oefeningen_Antwoord_AntwoordId",
                table: "Oefeningen",
                column: "AntwoordId",
                principalTable: "Antwoord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Oefeningen_GroepsBewerking_GroepsBewerkingId",
                table: "Oefeningen",
                column: "GroepsBewerkingId",
                principalTable: "GroepsBewerking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opdrachten_Oefeningen_OefeningId",
                table: "Opdrachten",
                column: "OefeningId",
                principalTable: "Oefeningen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oefeningen_Antwoord_AntwoordId",
                table: "Oefeningen");

            migrationBuilder.DropForeignKey(
                name: "FK_Oefeningen_GroepsBewerking_GroepsBewerkingId",
                table: "Oefeningen");

            migrationBuilder.DropForeignKey(
                name: "FK_Opdrachten_Oefeningen_OefeningId",
                table: "Opdrachten");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Oefeningen",
                table: "Oefeningen");

            migrationBuilder.RenameTable(
                name: "Oefeningen",
                newName: "Oefening");

            migrationBuilder.RenameIndex(
                name: "IX_Oefeningen_GroepsBewerkingId",
                table: "Oefening",
                newName: "IX_Oefening_GroepsBewerkingId");

            migrationBuilder.RenameIndex(
                name: "IX_Oefeningen_AntwoordId",
                table: "Oefening",
                newName: "IX_Oefening_AntwoordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Oefening",
                table: "Oefening",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Oefening_Antwoord_AntwoordId",
                table: "Oefening",
                column: "AntwoordId",
                principalTable: "Antwoord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Oefening_GroepsBewerking_GroepsBewerkingId",
                table: "Oefening",
                column: "GroepsBewerkingId",
                principalTable: "GroepsBewerking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opdrachten_Oefening_OefeningId",
                table: "Opdrachten",
                column: "OefeningId",
                principalTable: "Oefening",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}