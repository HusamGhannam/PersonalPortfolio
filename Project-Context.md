# Project Context — PersonalPortfolio

## Purpose
Personal portfolio website for Husam Ghannam. Displays projects, skills, certificates, and a contact form. Includes an admin panel for content management (CRUD projects/skills/certificates) and an authenticated user area.

## Tech Stack
- **Framework:** ASP.NET Core 10 (MVC + Blazor Server)
- **ORM:** Entity Framework Core 10 with SQLite (`portfolio.db`)
- **Auth:** ASP.NET Core Identity with roles (`Admin`)
- **UI:** Bootstrap 5, Font Awesome 6.5, jQuery, dark-tech theme, Inter + JetBrains Mono fonts, CSS custom properties, IntersectionObserver scroll animations
- **Target:** `net10.0`

## Key Packages
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` 10.0.10
- `Microsoft.AspNetCore.Identity.UI` 10.0.10
- `Microsoft.EntityFrameworkCore.Sqlite` 10.0.10
- `Microsoft.EntityFrameworkCore.Tools` 10.0.10

## Folder Structure
```
PersonalPortfolio/
├── Components/           # Blazor components
│   ├── ContactForm.razor
│   ├── ProjectCard.razor
│   ├── SampleProjects.razor
│   ├── SampleSkills.razor
│   └── SkillCard.razor
├── Controllers/
│   ├── AccountController.cs      # Login, Logout, ChangePassword
│   ├── HomeController.cs         # Public pages + Admin CRUD actions
│   └── Api/
│       ├── ContactController.cs
│       ├── ProjectController.cs
│       ├── SkillController.cs
│       └── CertificateController.cs
├── Data/
│   ├── AppDbContext.cs           # IdentityDbContext with seed data
│   └── SeedData.cs               # Admin user/role seeding (idempotent — only creates if missing)
├── Models/
│   ├── ApplicationUser.cs        # IdentityUser with DisplayName
│   ├── Certificate.cs            # Id, Title, Issuer, IssueDate, ExpiryDate?, CredentialId?, CredentialUrl?, Description?, ImageIconUrl?
│   ├── ChangePasswordViewModel.cs
│   ├── ContactMessage.cs
│   ├── LoginViewModel.cs
│   ├── Project.cs
│   ├── Skill.cs
│   └── ErrorViewModel.cs
├── Migrations/
├── Properties/
├── Services/
│   ├── ContactService.cs
│   ├── ProjectService.cs
│   ├── SkillService.cs
│   └── CertificateService.cs
├── Views/
│   ├── Account/
│   │   ├── Login.cshtml
│   │   └── ChangePassword.cshtml
│   ├── Home/
│   │   ├── Index.cshtml
│   │   ├── Projects.cshtml
│   │   ├── Skills.cshtml
│   │   ├── ContactMe.cshtml
│   │   ├── Privacy.cshtml
│   │   ├── EditProject.cshtml
│   │   ├── EditSkill.cshtml
│   │   ├── Certificates.cshtml
│   │   ├── EditCertificate.cshtml
│   │   └── _AdminPanel.cshtml    # Partial view for admin panel
│   └── Shared/
│       ├── _Layout.cshtml
│       └── Error.cshtml
├── ColorPallette.md              # Dark-tech color palette reference
├── wwwroot/
│   ├── css/site.css
│   ├── js/site.js
│   ├── lib/ (Bootstrap, jQuery, jQuery Validation)
│   └── favicon.ico
├── Program.cs
└── PersonalPortfolio.csproj
```

## Authentication & Authorization
- **Identity:** ASP.NET Core Identity with `ApplicationUser` (adds `DisplayName`).
- **Roles:** `Admin` role created by `SeedData`.
- **Policy:** `AdminOnly` requires `Admin` role.
- **Default admin credentials:** `admin@husamghannam.com` / `Admin@123`
- **Password policy (Program.cs):**
  - Min length: 6
  - Requires digit, uppercase, lowercase
  - Does NOT require non-alphanumeric
  - Lockout: 5 failed attempts, 15-min lockout
- **SeedData behavior:**
  - Runs on every app startup (`Program.cs` line 60)
  - Creates admin user if not found
  - **No longer resets the password** — SeedData only ensures the admin user and role exist; password changes via Change Password persist across restarts
  - Ensures admin role is assigned
- **Change Password feature:**
  - `AccountController.ChangePassword` GET/POST (both `[Authorize]`)
  - `ChangePasswordViewModel` — CurrentPassword, NewPassword, ConfirmPassword
  - `Views/Account/ChangePassword.cshtml` — form with validation
  - Navbar shows "Change Password" link when authenticated
  - Admin Panel has an "Account" section with Change Password button

## Database
- **SQLite** file: `portfolio.db` (auto-migrated on startup)
- **DbSets:** `ContactMessages`, `Projects`, `Skills`, `Certificates` (plus Identity tables)
- **Seed data (EF):** 3 sample projects, 3 sample skills, and 3 sample certificates
- **Seed data (Programmatic):** Admin user + role via `SeedData.InitializeAsync()`

## Controllers & Key Actions
- **HomeController:**
  - `Index` — renders admin panel partial if user is Admin
  - `Projects`, `Skills`, `Certificates`, `ContactMe`, `Privacy` — public views
  - `CreateProject/EditProject/DeleteProject` — `[Authorize(Policy = "AdminOnly")]`
  - `CreateSkill/EditSkill/DeleteSkill` — `[Authorize(Policy = "AdminOnly")]`
  - `CreateCertificate/EditCertificate/DeleteCertificate` — `[Authorize(Policy = "AdminOnly")]`
- **CertificateController (Api):**
  - `GET /api/certificate` — list all certificates
  - `GET /api/certificate/{id}` — get single certificate
  - `POST /api/certificate` — create (Admin only)
  - `PUT /api/certificate/{id}` — update (Admin only)
  - `DELETE /api/certificate/{id}` — delete (Admin only)
- **AccountController:**
  - `Login` (GET/POST), `Logout` (POST), `ChangePassword` (GET/POST, `[Authorize]`)

## Views & Layout
- **_Layout.cshtml:** Dark-themed navbar with Home, Projects, Skills, Certificates, Contact Me, Privacy links. Auth section shows username, Change Password link, and Logout when authenticated; Admin link when not.
- **Index.cshtml (Landing Page):** Full-viewport hero section with animated gradient text heading and particle background effect. Serves as the public-facing entry point.
- **_AdminPanel.cshtml:** Dark-themed partial on Index page. Account section (Change Password button), Projects table (edit/delete + create form), Skills table (edit/delete + create form), Certificates table (edit/delete + create form). Delete uses a Bootstrap modal confirmation.
- **Forms (EditProject, EditSkill, EditCertificate, Login, ChangePassword):** Dark-themed form styling consistent with the overall dark-tech palette.
- **Blazor:** Server-side Blazor via `MapBlazorHub()` + `blazor.server.js`. Components for contact form, project cards, skill cards.

## Coding Conventions
- C# file-scoped namespaces (`namespace X;`) for models/services, block-scoped for controllers
- ViewModels use `[Required]`, `[StringLength]`, `[DataType]`, `[Compare]` annotations
- Anti-forgery tokens on all POST forms (`@Html.AntiForgeryToken()` or `asp-antiforgery`)
- `TempData["SuccessMessage"]` / `TempData["ErrorMessage"]` for flash messages
- Services registered as `Scoped` via DI
- Auto-migrate + seed on startup in `Program.cs`
- **CSS custom properties** for all theme colors (defined in `:root`, referenced throughout `site.css`)
- **IntersectionObserver** for scroll-reveal animations on sections and cards
- **`prefers-reduced-motion`** media query respected — disables animations for users who prefer reduced motion

## Design System
- **Theme:** Dark-tech / dark industrial — deep charcoal backgrounds, neon accent highlights
- **Color Palette:** Documented in `ColorPallette.md` at project root. All colors exposed as CSS custom properties (e.g. `--bg-primary`, `--accent`, `--text-primary`)
- **Typography:** Inter (body/UI) + JetBrains Mono (code/monospace)
- **Palette reference file:** `ColorPallette.md` — use this as the source of truth when adding or adjusting colors

## Known Gotchas
- Admin password is **persisted permanently** — `SeedData` only creates the admin user/role if they don't exist and no longer resets the password. Changes via Change Password survive restarts.
- Old password `123557` did not meet Identity policy (no uppercase); current compliant password is `Admin@123`.
- SQLite is used for development; would need a different provider for production.
- Blazor Server is wired up but most views use Razor (MVC); Blazor components are embedded in MVC views.

## Commands
- **Run:** `dotnet run` (from project root)
- **Migrate:** `dotnet ef migrations add <Name>` / `dotnet ef database update`
- **Build:** `dotnet build`
