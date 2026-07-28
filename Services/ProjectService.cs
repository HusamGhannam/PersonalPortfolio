using Microsoft.EntityFrameworkCore;
using PersonalPortfolio.Data;
using PersonalPortfolio.Models;

namespace PersonalPortfolio.Services;

public interface IProjectService
{
    Task<List<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(int id);
    Task<Project> CreateAsync(Project project);
    Task<Project?> UpdateAsync(int id, Project project);
    Task<bool> DeleteAsync(int id);
}

public class ProjectService : IProjectService
{
    private readonly AppDbContext _context;

    public ProjectService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Project>> GetAllAsync()
    {
        return await _context.Projects
            .OrderByDescending(p => p.Id)
            .ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await _context.Projects.FindAsync(id);
    }

    public async Task<Project> CreateAsync(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return project;
    }

    public async Task<Project?> UpdateAsync(int id, Project project)
    {
        var existing = await _context.Projects.FindAsync(id);
        if (existing is null) return null;

        existing.Title = project.Title;
        existing.Description = project.Description;
        existing.TechStack = project.TechStack;
        existing.ImageUrl = project.ImageUrl;
        existing.LiveUrl = project.LiveUrl;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.Projects.FindAsync(id);
        if (existing is null) return false;

        _context.Projects.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
