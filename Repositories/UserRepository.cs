using api_rest_cs.Data;
using api_rest_cs.Models;
using Microsoft.EntityFrameworkCore;

namespace api_rest_cs.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users
                            .Include(u => u.Books)
                            .ToListAsync();
    }

    public async Task<User?> GetById(int id)
    {
        return await _context.Users
                            .Include(u => u.Books)
                            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task Update(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
