namespace PersonalPortfolio.Models;

public class AdminDashboardViewModel
{
    public List<Project> Projects { get; set; } = new();
    public List<Skill> Skills { get; set; } = new();
    public List<Certificate> Certificates { get; set; } = new();
}
