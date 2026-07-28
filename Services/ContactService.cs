using Microsoft.EntityFrameworkCore;
using PersonalPortfolio.Data;
using PersonalPortfolio.Models;

namespace PersonalPortfolio.Services;

public interface IContactService
{
    Task<ContactMessage> SaveMessageAsync(ContactMessage message);
    Task<List<ContactMessage>> GetAllMessagesAsync();
    Task<ContactMessage?> GetMessageByIdAsync(int id);
}

public class ContactService : IContactService
{
    private readonly AppDbContext _context;

    public ContactService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ContactMessage> SaveMessageAsync(ContactMessage message)
    {
        message.CreatedAt = DateTime.UtcNow;
        _context.ContactMessages.Add(message);
        await _context.SaveChangesAsync();
        return message;
    }

    public async Task<List<ContactMessage>> GetAllMessagesAsync()
    {
        return await _context.ContactMessages
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();
    }

    public async Task<ContactMessage?> GetMessageByIdAsync(int id)
    {
        return await _context.ContactMessages.FindAsync(id);
    }
}
