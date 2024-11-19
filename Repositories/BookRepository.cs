using api_rest_cs.Data;
using api_rest_cs.Models;
using Microsoft.EntityFrameworkCore;

namespace api_rest_cs.Repositories;

public class BookRepository(AppDbContext context) : IBookRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Book>> GetAll() => await _context.Books.ToListAsync();

    public async Task<Book?> GetById(int id) => await _context.Books.FindAsync(id);

    public async Task Add(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
