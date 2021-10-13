using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TILE03.Data.Migrations
{
    public partial class updateMappings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actie_UniekPad_UniekPadId",
                table: "Actie");

            migrationBuilder.DropForeignKey(
                name: "FK_Antwoord_GroepsBewerking_BewerkingId",
                table: "Antwoord");

            migrationBuilder.DropForeignKey(
                name: "FK_Opdracht_Oefening_OefeningId",
                table: "Opdracht");

            migrationBuilder.DropForeignKey(
                name: "FK_Opdracht_Toegangscode_ToegangscodeId",
                table: "Opdracht");

            migrationBuilder.DropForeignKey(
                name: "FK_Opdracht_UniekPad_UniekPadId",
                table: "Opdracht");

            migrationBuilder.DropForeignKey(
                name: "FK_UniekPad_Groepen_GroepId",
                table: "UniekPad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UniekPad",
                table: "UniekPad");

            migrationBuilder.DropIndex(
                name: "IX_UniekPad_GroepId",
                table: "UniekPad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Opdracht",
                table: "Opdracht");

            migrationBuilder.DropIndex(
                name: "IX_Antwoord_BewerkingId",
                table: "Antwoord");

            migrationBuilder.DropColumn(
                name: "GroepId",
                table: "UniekPad");

            migrationBuilder.DropColumn(
                name: "BewerkingId",
                table: "Antwoord");

            migrationBuilder.RenameTable(
                name: "UniekPad",
                newName: "UniekePaden");

            migrationBuilder.RenameTable(
                name: "Opdracht",
                newName: "Opdrachten");

            migrationBuilder.RenameIndex(
                name: "IX_Opdracht_UniekPadId",
                table: "Opdrachten",
                newName: "IX_Opdrachten_UniekPadId");

            migrationBuilder.RenameIndex(
                name: "IX_Opdracht_ToegangscodeId",
                table: "Opdrachten",
                newName: "IX_Opdrachten_ToegangscodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Opdracht_OefeningId",
                table: "Opdrachten",
                newName: "IX_Opdrachten_OefeningId");

            migrationBuilder.AddColumn<bool>(
                name: "IsOpgelost",
                table: "Opdrachten",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "GroepsBewerkingId",
                table: "Oefening",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniekePadId",
                table: "Groepen",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UniekePaden",
                table: "UniekePaden",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Opdrachten",
                table: "Opdrachten",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Oefening_GroepsBewerkingId",
                table: "Oefening",
                column: "GroepsBewerkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Groepen_UniekePadId",
                table: "Groepen",
                column: "UniekePadId",
                unique: true,
                filter: "[UniekePadId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Actie_UniekePaden_UniekPadId",
                table: "Actie",
                column: "UniekPadId",
                principalTable: "UniekePaden",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groepen_UniekePaden_UniekePadId",
                table: "Groepen",
                column: "UniekePadId",
                principalTable: "UniekePaden",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Opdrachten_Toegangscode_ToegangscodeId",
                table: "Opdrachten",
                column: "ToegangscodeId",
                principalTable: "Toegangscode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opdrachten_UniekePaden_UniekPadId",
                table: "Opdrachten",
                column: "UniekPadId",
                principalTable: "UniekePaden",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actie_UniekePaden_UniekPadId",
                table: "Actie");

            migrationBuilder.DropForeignKey(
                name: "FK_Groepen_UniekePaden_UniekePadId",
                table: "Groepen");

            migrationBuilder.DropForeignKey(
                name: "FK_Oefening_GroepsBewerking_GroepsBewerkingId",
                table: "Oefening");

            migrationBuilder.DropForeignKey(
                name: "FK_Opdrachten_Oefening_OefeningId",
                table: "Opdrachten");

            migrationBuilder.DropForeignKey(
                name: "FK_Opdrachten_Toegangscode_ToegangscodeId",
                table: "Opdrachten");

            migrationBuilder.DropForeignKey(
                name: "FK_Opdrachten_UniekePaden_UniekPadId",
                table: "Opdrachten");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UniekePaden",
                table: "UniekePaden");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Opdrachten",
                table: "Opdrachten");

            migrationBuilder.DropIndex(
                name: "IX_Oefening_GroepsBewerkingId",
                table: "Oefening");

            migrationBuilder.DropIndex(
                name: "IX_Groepen_UniekePadId",
                table: "Groepen");

            migrationBuilder.DropColumn(
                name: "IsOpgelost",
                table: "Opdrachten");

            migrationBuilder.DropColumn(
                name: "GroepsBewerkingId",
                table: "Oefening");

            migrationBuilder.DropColumn(
                name: "UniekePadId",
                table: "Groepen");

            migrationBuilder.RenameTable(
                name: "UniekePaden",
                newName: "UniekPad");

            migrationBuilder.RenameTable(
                name: "Opdrachten",
                newName: "Opdracht");

            migrationBuilder.RenameIndex(
                name: "IX_Opdrachten_UniekPadId",
                table: "Opdracht",
                newName: "IX_Opdracht_UniekPadId");

            migrationBuilder.RenameIndex(
                name: "IX_Opdrachten_ToegangscodeId",
                table: "Opdracht",
                newName: "IX_Opdracht_ToegangscodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Opdrachten_OefeningId",
                table: "Opdracht",
                newName: "IX_Opdracht_OefeningId");

            migrationBuilder.AddColumn<int>(
                name: "GroepId",
                table: "UniekPad",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BewerkingId",
                table: "Antwoord",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UniekPad",
                table: "UniekPad",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Opdracht",
                table: "Opdracht",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UniekPad_GroepId",
                table: "UniekPad",
                column: "GroepId");

            migrationBuilder.CreateIndex(
                name: "IX_Antwoord_BewerkingId",
                table: "Antwoord",
                column: "BewerkingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actie_UniekPad_UniekPadId",
                table: "Actie",
                column: "UniekPadId",
                principalTable: "UniekPad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Antwoord_GroepsBewerking_BewerkingId",
                table: "Antwoord",
                column: "BewerkingId",
                principalTable: "GroepsBewerking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opdracht_Oefening_OefeningId",
                table: "Opdracht",
                column: "OefeningId",
                principalTable: "Oefening",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opdracht_Toegangscode_ToegangscodeId",
                table: "Opdracht",
                column: "ToegangscodeId",
                principalTable: "Toegangscode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opdracht_UniekPad_UniekPadId",
                table: "Opdracht",
                column: "UniekPadId",
                principalTable: "UniekPad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UniekPad_Groepen_GroepId",
                table: "UniekPad",
                column: "GroepId",
                principalTable: "Groepen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}