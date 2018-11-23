using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartFlow.Shared.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedDate = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    LastUpdatedDate = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Declarations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedDate = table.Column<string>(nullable: true),
                    DeNo = table.Column<string>(nullable: true),
                    DeclarationType = table.Column<int>(nullable: false),
                    LastUpdatedDate = table.Column<string>(nullable: true),
                    ProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Declarations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Declarations_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Question_Dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AnswerCode = table.Column<string>(nullable: true),
                    AnswerText = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<string>(nullable: true),
                    Declaration_DboId = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<string>(nullable: true),
                    Profile_DboId = table.Column<int>(nullable: true),
                    QuestionIdentifier = table.Column<int>(nullable: false),
                    QuestionKey = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question_Dbo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Dbo_Declarations_Declaration_DboId",
                        column: x => x.Declaration_DboId,
                        principalTable: "Declarations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Question_Dbo_Profiles_Profile_DboId",
                        column: x => x.Profile_DboId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Declarations_ProfileId",
                table: "Declarations",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_Dbo_Declaration_DboId",
                table: "Question_Dbo",
                column: "Declaration_DboId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_Dbo_Profile_DboId",
                table: "Question_Dbo",
                column: "Profile_DboId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Question_Dbo");

            migrationBuilder.DropTable(
                name: "Declarations");

            migrationBuilder.DropTable(
                name: "Profiles");
        }
    }
}
