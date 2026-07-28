using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalPortfolio.Models;
using PersonalPortfolio.Services;

namespace PersonalPortfolio.Controllers.Api;

/// <summary>
/// API controller for managing portfolio projects.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    /// <summary>
    /// Gets all projects.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var projects = await _projectService.GetAllAsync();
        return Ok(projects);
    }

    /// <summary>
    /// Gets a project by its identifier.
    /// </summary>
    /// <param name="id">The project identifier.</param>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var project = await _projectService.GetByIdAsync(id);
        if (project is null)
            return NotFound(new { message = $"Project with id {id} not found." });

        return Ok(project);
    }

    /// <summary>
    /// Creates a new project.
    /// </summary>
    /// <param name="project">The project data.</param>
    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Create([FromBody] Project project)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _projectService.CreateAsync(project);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Updates an existing project.
    /// </summary>
    /// <param name="id">The project identifier.</param>
    /// <param name="project">The updated project data.</param>
    [HttpPut("{id:int}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Update(int id, [FromBody] Project project)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _projectService.UpdateAsync(id, project);
        if (updated is null)
            return NotFound(new { message = $"Project with id {id} not found." });

        return Ok(updated);
    }

    /// <summary>
    /// Deletes a project.
    /// </summary>
    /// <param name="id">The project identifier.</param>
    [HttpDelete("{id:int}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _projectService.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = $"Project with id {id} not found." });

        return NoContent();
    }
}
