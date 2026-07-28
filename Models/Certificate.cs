using System.ComponentModel.DataAnnotations;

namespace PersonalPortfolio.Models;

public class Certificate
{
    public int Id { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Issuer { get; set; } = string.Empty;

    public DateTime IssueDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    [StringLength(200)]
    public string? CredentialId { get; set; }

    [StringLength(500)]
    public string? CredentialUrl { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [StringLength(500)]
    public string? ImageIconUrl { get; set; }
}
