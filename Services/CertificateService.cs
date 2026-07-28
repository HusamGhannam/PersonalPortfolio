using Microsoft.EntityFrameworkCore;
using PersonalPortfolio.Data;
using PersonalPortfolio.Models;

namespace PersonalPortfolio.Services;

public interface ICertificateService
{
    Task<List<Certificate>> GetAllAsync();
    Task<Certificate?> GetByIdAsync(int id);
    Task<Certificate> CreateAsync(Certificate certificate);
    Task<Certificate?> UpdateAsync(int id, Certificate certificate);
    Task<bool> DeleteAsync(int id);
}

public class CertificateService : ICertificateService
{
    private readonly AppDbContext _context;

    public CertificateService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Certificate>> GetAllAsync()
    {
        return await _context.Certificates
            .OrderByDescending(c => c.IssueDate)
            .ToListAsync();
    }

    public async Task<Certificate?> GetByIdAsync(int id)
    {
        return await _context.Certificates.FindAsync(id);
    }

    public async Task<Certificate> CreateAsync(Certificate certificate)
    {
        _context.Certificates.Add(certificate);
        await _context.SaveChangesAsync();
        return certificate;
    }

    public async Task<Certificate?> UpdateAsync(int id, Certificate certificate)
    {
        var existing = await _context.Certificates.FindAsync(id);
        if (existing is null) return null;

        existing.Title = certificate.Title;
        existing.Issuer = certificate.Issuer;
        existing.IssueDate = certificate.IssueDate;
        existing.ExpiryDate = certificate.ExpiryDate;
        existing.CredentialId = certificate.CredentialId;
        existing.CredentialUrl = certificate.CredentialUrl;
        existing.Description = certificate.Description;
        existing.ImageIconUrl = certificate.ImageIconUrl;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.Certificates.FindAsync(id);
        if (existing is null) return false;

        _context.Certificates.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
