using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PersonalPortfolio.Models;

namespace PersonalPortfolio.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<Certificate> Certificates => Set<Certificate>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ContactMessage>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Subject).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Message).IsRequired().HasMaxLength(2000);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.TechStack).IsRequired().HasMaxLength(500);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.LiveUrl).HasMaxLength(500);
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
            entity.Property(e => e.IconClass).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Issuer).IsRequired().HasMaxLength(200);
            entity.Property(e => e.CredentialId).HasMaxLength(200);
            entity.Property(e => e.CredentialUrl).HasMaxLength(500);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.ImageIconUrl).HasMaxLength(500);
        });

        // Seed data
        modelBuilder.Entity<Project>().HasData(
            new Project
            {
                Id = 1,
                Title = "E-Commerce Platform",
                Description = "A full-stack e-commerce solution with product catalog, shopping cart, and secure checkout powered by Stripe.",
                TechStack = "ASP.NET Core, Blazor, EF Core, SQL Server, Stripe",
                ImageUrl = "https://placehold.co/600x400/1b6ec2/ffffff?text=E-Commerce",
                LiveUrl = "https://example.com/ecommerce"
            },
            new Project
            {
                Id = 2,
                Title = "Task Management App",
                Description = "A collaborative task management application with real-time updates, drag-and-drop boards, and team workspaces.",
                TechStack = "ASP.NET Core, SignalR, React, PostgreSQL",
                ImageUrl = "https://placehold.co/600x400/28a745/ffffff?text=Task+Manager",
                LiveUrl = "https://example.com/tasks"
            },
            new Project
            {
                Id = 3,
                Title = "Weather Dashboard",
                Description = "A responsive weather dashboard that displays current conditions, forecasts, and interactive maps using external APIs.",
                TechStack = "Blazor, Chart.js, OpenWeather API",
                ImageUrl = "https://placehold.co/600x400/dc3545/ffffff?text=Weather+App",
                LiveUrl = ""
            });

        modelBuilder.Entity<Skill>().HasData(
            new Skill
            {
                Id = 1,
                Name = "Agentic AI & RAG",
                Category = "AI",
                ProficiencyLevel = 3,
                IconClass = "fas fa-robot"
            },
            new Skill
            {
                Id = 2,
                Name = "ASP.NET Core Backend Developer",
                Category = "Backend",
                ProficiencyLevel = 5,
                IconClass = "fas fa-server"
            },
            new Skill
            {
                Id = 3,
                Name = "Frontend Developer",
                Category = "Frontend",
                ProficiencyLevel = 3,
                IconClass = "fas fa-code"
            });

        modelBuilder.Entity<Certificate>().HasData(
            new Certificate
            {
                Id = 1,
                Title = "Microsoft Certified: Azure Developer Associate",
                Issuer = "Microsoft",
                IssueDate = new DateTime(2024, 3, 15),
                ExpiryDate = new DateTime(2026, 3, 15),
                CredentialId = "AZ-204-2024-001",
                CredentialUrl = "https://learn.microsoft.com/certifications/azure-developer",
                Description = "Demonstrated expertise in designing, building, testing, and maintaining cloud applications and services on Microsoft Azure.",
                ImageIconUrl = "https://placehold.co/100x100/0078d4/ffffff?text=AZ"
            },
            new Certificate
            {
                Id = 2,
                Title = "AWS Certified Solutions Architect - Associate",
                Issuer = "Amazon Web Services",
                IssueDate = new DateTime(2023, 11, 20),
                ExpiryDate = new DateTime(2026, 11, 20),
                CredentialId = "AWS-SAA-2023-002",
                CredentialUrl = "https://aws.amazon.com/certification/certified-solutions-architect-associate/",
                Description = "Validated technical expertise in deploying fault-tolerant and highly available systems on the AWS platform.",
                ImageIconUrl = "https://placehold.co/100x100/ff9900/ffffff?text=AWS"
            },
            new Certificate
            {
                Id = 3,
                Title = "EF Core Deep Dive",
                Issuer = "Pluralsight",
                IssueDate = new DateTime(2023, 6, 10),
                ExpiryDate = null,
                CredentialId = null,
                CredentialUrl = "https://www.pluralsight.com/courses/ef-core-deep-dive",
                Description = "Completed comprehensive training on Entity Framework Core covering migrations, performance tuning, and advanced querying patterns.",
                ImageIconUrl = "https://placehold.co/100x100/e80a89/ffffff?text=PS"
            });
    }
}
