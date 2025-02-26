using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace System.Migrations
{
    public partial class V001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "afs");

            migrationBuilder.CreateTable(
                name: "AttributeDescriptors",
                schema: "afs",
                columns: table => new
                {
                    AttributeName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AttributeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredOnCreation = table.Column<bool>(type: "bit", nullable: false),
                    Updatable = table.Column<bool>(type: "bit", nullable: false),
                    IsManagedValueRange = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeDescriptors", x => x.AttributeName);
                });

            migrationBuilder.CreateTable(
                name: "FileContentBlobs",
                schema: "afs",
                columns: table => new
                {
                    FileId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RawContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileContentBlobs", x => x.FileId);
                });

            migrationBuilder.CreateTable(
                name: "AllowedValues",
                schema: "afs",
                columns: table => new
                {
                    AttributeName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowedValues", x => new { x.AttributeName, x.Value });
                    table.ForeignKey(
                        name: "FK_AllowedValues_AttributeDescriptors_AttributeName",
                        column: x => x.AttributeName,
                        principalSchema: "afs",
                        principalTable: "AttributeDescriptors",
                        principalColumn: "AttributeName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileAttributes",
                schema: "afs",
                columns: table => new
                {
                    FileId = table.Column<long>(type: "bigint", nullable: false),
                    AttributeName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AttributeValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAttributes", x => new { x.FileId, x.AttributeName });
                    table.ForeignKey(
                        name: "FK_FileAttributes_AttributeDescriptors_AttributeName",
                        column: x => x.AttributeName,
                        principalSchema: "afs",
                        principalTable: "AttributeDescriptors",
                        principalColumn: "AttributeName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileAttributes_FileContentBlobs_FileId",
                        column: x => x.FileId,
                        principalSchema: "afs",
                        principalTable: "FileContentBlobs",
                        principalColumn: "FileId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileAttributes_AttributeName",
                schema: "afs",
                table: "FileAttributes",
                column: "AttributeName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllowedValues",
                schema: "afs");

            migrationBuilder.DropTable(
                name: "FileAttributes",
                schema: "afs");

            migrationBuilder.DropTable(
                name: "AttributeDescriptors",
                schema: "afs");

            migrationBuilder.DropTable(
                name: "FileContentBlobs",
                schema: "afs");
        }
    }
}
