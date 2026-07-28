using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalPortfolio.Models;
using PersonalPortfolio.Services;

namespace PersonalPortfolio.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class SkillController : ControllerBase
{
    private readonly ISkillService _skillService;

    public SkillController(ISkillService skillService)
    {
        _skillService = skillService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var skills = await _skillService.GetAllAsync();
        return Ok(skills);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var skill = await _skillService.GetByIdAsync(id);
        if (skill is null)
            return NotFound(new { message = $"Skill with id {id} not found." });

        return Ok(skill);
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Create([FromBody] Skill skill)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _skillService.CreateAsync(skill);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }


    [HttpPut("{id:int}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Update(int id, [FromBody] Skill skill)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _skillService.UpdateAsync(id, skill);
        if (updated is null)
            return NotFound(new { message = $"Skill with id {id} not found." });

        return Ok(updated);
    }


    [HttpDelete("{id:int}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _skillService.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = $"Skill with id {id} not found." });

        return NoContent();
    }
}
