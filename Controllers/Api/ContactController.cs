using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using PersonalPortfolio.Models;
using PersonalPortfolio.Services;

namespace PersonalPortfolio.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("ContactForm")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpPost]
    public async Task<IActionResult> Submit([FromBody] ContactMessage message)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var saved = await _contactService.SaveMessageAsync(message);
        return Ok(new { success = true, id = saved.Id, message = "Message sent successfully." });
    }

    [HttpGet]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> GetAll()
    {
        var messages = await _contactService.GetAllMessagesAsync();
        return Ok(messages);
    }
}
