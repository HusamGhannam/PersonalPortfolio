using Microsoft.EntityFrameworkCore;
using PersonalPortfolio.Data;
using PersonalPortfolio.Models;

namespace PersonalPortfolio.Services;

public interface ISkillService
{
    Task<List<Skill>> GetAllAsync();
    Task<Skill?> GetByIdAsync(int id);
    Task<Skill> CreateAsync(Skill skill);
    Task<Skill?> UpdateAsync(int id, Skill skill);
    Task<bool> DeleteAsync(int id);
}

public class SkillService : ISkillService
{
    private readonly AppDbContext _context;

    public SkillService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Skill>> GetAllAsync()
    {
        return await _context.Skills
            .OrderBy(s => s.Category)
            .ThenBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<Skill?> GetByIdAsync(int id)
    {
        return await _context.Skills.FindAsync(id);
    }

    public async Task<Skill> CreateAsync(Skill skill)
    {
        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();
        return skill;
    }

    public async Task<Skill?> UpdateAsync(int id, Skill skill)
    {
        var existing = await _context.Skills.FindAsync(id);
        if (existing is null) return null;

        existing.Name = skill.Name;
        existing.Category = skill.Category;
        existing.ProficiencyLevel = skill.ProficiencyLevel;
        existing.IconClass = skill.IconClass;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.Skills.FindAsync(id);
        if (existing is null) return false;

        _context.Skills.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
