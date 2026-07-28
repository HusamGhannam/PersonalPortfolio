using System.ComponentModel.DataAnnotations;

namespace PersonalPortfolio.Models;

public class Project
{
    public int Id { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(1000, MinimumLength = 1)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [StringLength(500, MinimumLength = 1)]
    public string TechStack { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }

    public string? LiveUrl { get; set; }
}
