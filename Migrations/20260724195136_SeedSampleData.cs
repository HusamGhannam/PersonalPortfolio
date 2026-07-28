using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonalPortfolio.Migrations
{
    /// <inheritdoc />
    public partial class SeedSampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Description", "ImageUrl", "LiveUrl", "TechStack", "Title" },
                values: new object[,]
                {
                    { 1, "A full-stack e-commerce solution with product catalog, shopping cart, and secure checkout powered by Stripe.", "https://placehold.co/600x400/1b6ec2/ffffff?text=E-Commerce", "https://example.com/ecommerce", "ASP.NET Core, Blazor, EF Core, SQL Server, Stripe", "E-Commerce Platform" },
                    { 2, "A collaborative task management application with real-time updates, drag-and-drop boards, and team workspaces.", "https://placehold.co/600x400/28a745/ffffff?text=Task+Manager", "https://example.com/tasks", "ASP.NET Core, SignalR, React, PostgreSQL", "Task Management App" },
                    { 3, "A responsive weather dashboard that displays current conditions, forecasts, and interactive maps using external APIs.", "https://placehold.co/600x400/dc3545/ffffff?text=Weather+App", "", "Blazor, Chart.js, OpenWeather API", "Weather Dashboard" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Category", "IconClass", "Name", "ProficiencyLevel" },
                values: new object[,]
                {
                    { 1, "AI", "fas fa-robot", "Agentic AI & RAG", 3 },
                    { 2, "Backend", "fas fa-server", "ASP.NET Core Backend Developer", 5 },
                    { 3, "Frontend", "fas fa-code", "Frontend Developer", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
