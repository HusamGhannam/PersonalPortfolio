using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalPortfolio.Models;
using PersonalPortfolio.Services;

namespace PersonalPortfolio.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class CertificateController : ControllerBase
{
    private readonly ICertificateService _certificateService;

    public CertificateController(ICertificateService certificateService)
    {
        _certificateService = certificateService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var certificates = await _certificateService.GetAllAsync();
        return Ok(certificates);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var certificate = await _certificateService.GetByIdAsync(id);
        if (certificate is null)
            return NotFound(new { message = $"Certificate with id {id} not found." });

        return Ok(certificate);
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Create([FromBody] Certificate certificate)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _certificateService.CreateAsync(certificate);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Update(int id, [FromBody] Certificate certificate)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _certificateService.UpdateAsync(id, certificate);
        if (updated is null)
            return NotFound(new { message = $"Certificate with id {id} not found." });

        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _certificateService.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = $"Certificate with id {id} not found." });

        return NoContent();
    }
}
