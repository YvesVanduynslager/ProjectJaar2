using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TILE03.Data.Migrations
{
    public partial class initMappings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateTable(
                name: "GroepsBewerking",
                columns: table => new
                {
                    Param = table.Column<double>(nullable: true),
                    Delen_Param = table.Column<double>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: false),
                    Optellen_Param = table.Column<double>(nullable: true),
                    Vermenigvuldig_Param = table.Column<double>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_GroepsBewerking", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "Klas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Klas", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "Toegangscode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Toegangscode", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "Antwoord",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    BewerkingId = table.Column<int>(nullable: true),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Antwoord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Antwoord_GroepsBewerking_BewerkingId",
                        column: x => x.BewerkingId,
                        principalTable: "GroepsBewerking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sessies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    KlasId = table.Column<int>(nullable: true),
                    Naam = table.Column<string>(nullable: true),
                    Omschrijving = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessies_Klas_KlasId",
                        column: x => x.KlasId,
                        principalTable: "Klas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Oefening",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    AntwoordId = table.Column<int>(nullable: true),
                    Naam = table.Column<string>(nullable: true),
                    OpgaveFile = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oefening", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Oefening_Antwoord_AntwoordId",
                        column: x => x.AntwoordId,
                        principalTable: "Antwoord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Groepen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    SessieId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groepen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groepen_Sessies_SessieId",
                        column: x => x.SessieId,
                        principalTable: "Sessies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Leerling",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    GroepId = table.Column<int>(nullable: true),
                    KlasId = table.Column<int>(nullable: true),
                    Naam = table.Column<string>(nullable: true),
                    Voornam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leerling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leerling_Groepen_GroepId",
                        column: x => x.GroepId,
                        principalTable: "Groepen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leerling_Klas_KlasId",
                        column: x => x.KlasId,
                        principalTable: "Klas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UniekPad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    GroepId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniekPad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UniekPad_Groepen_GroepId",
                        column: x => x.GroepId,
                        principalTable: "Groepen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Actie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Omschrijving = table.Column<string>(nullable: true),
                    UniekPadId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actie_UniekPad_UniekPadId",
                        column: x => x.UniekPadId,
                        principalTable: "UniekPad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Opdracht",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    OefeningId = table.Column<int>(nullable: true),
                    ToegangscodeId = table.Column<int>(nullable: true),
                    UniekPadId = table.Column<int>(nullable: true),
                    VolgNr = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opdracht", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opdracht_Oefening_OefeningId",
                        column: x => x.OefeningId,
                        principalTable: "Oefening",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opdracht_Toegangscode_ToegangscodeId",
                        column: x => x.ToegangscodeId,
                        principalTable: "Toegangscode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opdracht_UniekPad_UniekPadId",
                        column: x => x.UniekPadId,
                        principalTable: "UniekPad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Actie_UniekPadId",
                table: "Actie",
                column: "UniekPadId");

            migrationBuilder.CreateIndex(
                name: "IX_Antwoord_BewerkingId",
                table: "Antwoord",
                column: "BewerkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Groepen_SessieId",
                table: "Groepen",
                column: "SessieId");

            migrationBuilder.CreateIndex(
                name: "IX_Leerling_GroepId",
                table: "Leerling",
                column: "GroepId");

            migrationBuilder.CreateIndex(
                name: "IX_Leerling_KlasId",
                table: "Leerling",
                column: "KlasId");

            migrationBuilder.CreateIndex(
                name: "IX_Oefening_AntwoordId",
                table: "Oefening",
                column: "AntwoordId",
                unique: true,
                filter: "[AntwoordId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Opdracht_OefeningId",
                table: "Opdracht",
                column: "OefeningId");

            migrationBuilder.CreateIndex(
                name: "IX_Opdracht_ToegangscodeId",
                table: "Opdracht",
                column: "ToegangscodeId",
                unique: true,
                filter: "[ToegangscodeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Opdracht_UniekPadId",
                table: "Opdracht",
                column: "UniekPadId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessies_KlasId",
                table: "Sessies",
                column: "KlasId",
                unique: true,
                filter: "[KlasId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UniekPad_GroepId",
                table: "UniekPad",
                column: "GroepId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Actie");

            migrationBuilder.DropTable(
                name: "Leerling");

            migrationBuilder.DropTable(
                name: "Opdracht");

            migrationBuilder.DropTable(
                name: "Oefening");

            migrationBuilder.DropTable(
                name: "Toegangscode");

            migrationBuilder.DropTable(
                name: "UniekPad");

            migrationBuilder.DropTable(
                name: "Antwoord");

            migrationBuilder.DropTable(
                name: "Groepen");

            migrationBuilder.DropTable(
                name: "GroepsBewerking");

            migrationBuilder.DropTable(
                name: "Sessies");

            migrationBuilder.DropTable(
                name: "Klas");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}