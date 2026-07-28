using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalPortfolio.Models;
using PersonalPortfolio.Services;

namespace PersonalPortfolio.Controllers;

[Authorize(Policy = "AdminOnly")]
public class AdminController : Controller
{
    private readonly IProjectService _projectService;
    private readonly ISkillService _skillService;
    private readonly ICertificateService _certificateService;

    public AdminController(
        IProjectService projectService,
        ISkillService skillService,
        ICertificateService certificateService)
    {
        _projectService = projectService;
        _skillService = skillService;
        _certificateService = certificateService;
    }

    // =====================
    // Dashboard
    // =====================

    public async Task<IActionResult> Index()
    {
        var projects = await _projectService.GetAllAsync();
        var skills = await _skillService.GetAllAsync();
        var certificates = await _certificateService.GetAllAsync();

        return View(new AdminDashboardViewModel
        {
            Projects = projects,
            Skills = skills,
            Certificates = certificates
        });
    }

    // =====================
    // Projects
    // =====================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProject(Project project)
    {
        if (ModelState.IsValid)
        {
            await _projectService.CreateAsync(project);
            TempData["SuccessMessage"] = "Project created successfully!";
            return RedirectToAction(nameof(Index));
        }

        TempData["ErrorMessage"] = "Failed to create project. Please check the form.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> EditProject(int id)
    {
        var project = await _projectService.GetByIdAsync(id);
        if (project is null)
            return NotFound();

        return View(project);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProject(int id, Project project)
    {
        if (ModelState.IsValid)
        {
            var updated = await _projectService.UpdateAsync(id, project);
            if (updated is not null)
            {
                TempData["SuccessMessage"] = "Project updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Project not found.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to update project. Please check the form.";
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var deleted = await _projectService.DeleteAsync(id);
        if (deleted)
            TempData["SuccessMessage"] = "Project deleted successfully!";
        else
            TempData["ErrorMessage"] = "Project not found.";

        return RedirectToAction(nameof(Index));
    }

    // =====================
    // Skills
    // =====================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateSkill(Skill skill)
    {
        if (ModelState.IsValid)
        {
            await _skillService.CreateAsync(skill);
            TempData["SuccessMessage"] = "Skill created successfully!";
            return RedirectToAction(nameof(Index));
        }

        TempData["ErrorMessage"] = "Failed to create skill. Please check the form.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> EditSkill(int id)
    {
        var skill = await _skillService.GetByIdAsync(id);
        if (skill is null)
            return NotFound();

        return View(skill);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditSkill(int id, Skill skill)
    {
        if (ModelState.IsValid)
        {
            var updated = await _skillService.UpdateAsync(id, skill);
            if (updated is not null)
            {
                TempData["SuccessMessage"] = "Skill updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Skill not found.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to update skill. Please check the form.";
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteSkill(int id)
    {
        var deleted = await _skillService.DeleteAsync(id);
        if (deleted)
            TempData["SuccessMessage"] = "Skill deleted successfully!";
        else
            TempData["ErrorMessage"] = "Skill not found.";

        return RedirectToAction(nameof(Index));
    }

    // =====================
    // Certificates
    // =====================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCertificate(Certificate certificate)
    {
        if (ModelState.IsValid)
        {
            await _certificateService.CreateAsync(certificate);
            TempData["SuccessMessage"] = "Certificate created successfully!";
            return RedirectToAction(nameof(Index));
        }

        TempData["ErrorMessage"] = "Failed to create certificate. Please check the form.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> EditCertificate(int id)
    {
        var certificate = await _certificateService.GetByIdAsync(id);
        if (certificate is null)
            return NotFound();

        return View(certificate);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditCertificate(int id, Certificate certificate)
    {
        if (ModelState.IsValid)
        {
            var updated = await _certificateService.UpdateAsync(id, certificate);
            if (updated is not null)
            {
                TempData["SuccessMessage"] = "Certificate updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Certificate not found.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to update certificate. Please check the form.";
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCertificate(int id)
    {
        var deleted = await _certificateService.DeleteAsync(id);
        if (deleted)
            TempData["SuccessMessage"] = "Certificate deleted successfully!";
        else
            TempData["ErrorMessage"] = "Certificate not found.";

        return RedirectToAction(nameof(Index));
    }
}
