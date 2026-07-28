using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonalPortfolio.Migrations
{
    /// <inheritdoc />
    public partial class AddCertificatesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Issuer = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CredentialId = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    CredentialUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ImageIconUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Certificates",
                columns: new[] { "Id", "CredentialId", "CredentialUrl", "Description", "ExpiryDate", "ImageIconUrl", "IssueDate", "Issuer", "Title" },
                values: new object[,]
                {
                    { 1, "AZ-204-2024-001", "https://learn.microsoft.com/certifications/azure-developer", "Demonstrated expertise in designing, building, testing, and maintaining cloud applications and services on Microsoft Azure.", new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://placehold.co/100x100/0078d4/ffffff?text=AZ", new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Microsoft", "Microsoft Certified: Azure Developer Associate" },
                    { 2, "AWS-SAA-2023-002", "https://aws.amazon.com/certification/certified-solutions-architect-associate/", "Validated technical expertise in deploying fault-tolerant and highly available systems on the AWS platform.", new DateTime(2026, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://placehold.co/100x100/ff9900/ffffff?text=AWS", new DateTime(2023, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Amazon Web Services", "AWS Certified Solutions Architect - Associate" },
                    { 3, null, "https://www.pluralsight.com/courses/ef-core-deep-dive", "Completed comprehensive training on Entity Framework Core covering migrations, performance tuning, and advanced querying patterns.", null, "https://placehold.co/100x100/e80a89/ffffff?text=PS", new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pluralsight", "EF Core Deep Dive" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificates");
        }
    }
}
