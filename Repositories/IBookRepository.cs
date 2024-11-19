using api_rest_cs.Models;

namespace api_rest_cs.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAll();
    Task<Book?> GetById(int id);
    Task Add(Book book);
    Task Update(Book book);
    Task Delete(int id);
}
