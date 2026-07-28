using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PersonalPortfolio.Models;
using PersonalPortfolio.Services;

namespace PersonalPortfolio.Controllers;

public class HomeController : Controller
{
    private readonly IProjectService _projectService;
    private readonly ISkillService _skillService;
    private readonly ICertificateService _certificateService;

    public HomeController(
        IProjectService projectService,
        ISkillService skillService,
        ICertificateService certificateService)
    {
        _projectService = projectService;
        _skillService = skillService;
        _certificateService = certificateService;
    }

    public async Task<IActionResult> Index()
    {
        var projects = await _projectService.GetAllAsync();
        var skills = await _skillService.GetAllAsync();
        var certificates = await _certificateService.GetAllAsync();

        int projectCount = projects.Count;
        int skillCount = skills.Count;
        int certCount = certificates.Count;

        ViewBag.ProjectCount = projectCount;
        ViewBag.SkillCount = skillCount;
        ViewBag.CertCount = certCount;

        return View();
    }

    public async Task<IActionResult> Projects()
    {
        var projects = await _projectService.GetAllAsync();
        return View(projects);
    }

    public async Task<IActionResult> Skills()
    {
        var skills = await _skillService.GetAllAsync();
        return View(skills);
    }

    public async Task<IActionResult> Certificates()
    {
        var certificates = await _certificateService.GetAllAsync();
        return View(certificates);
    }

    public IActionResult ContactMe()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
