using api_rest_cs.Models;
using api_rest_cs.Repositories;

namespace api_rest_cs.Services;

public class BookService(IBookRepository repository)
{
    private readonly IBookRepository _repository = repository;

    public async Task<IEnumerable<Book>> GetAll() => await _repository.GetAll();

    public async Task<Book?> GetById(int id) => await _repository.GetById(id);

    public async Task Add(Book book) => await _repository.Add(book);

    public async Task Update(Book book) => await _repository.Update(book);

    public async Task Delete(int id) => await _repository.Delete(id);
}