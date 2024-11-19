using api_rest_cs.Models;
using api_rest_cs.Repositories;

namespace api_rest_cs.Services;

public class UserService(IUserRepository repository)
{
    private readonly IUserRepository _repository = repository;

    public async Task<IEnumerable<User>> GetAll() => await _repository.GetAll();

    public async Task<User?> GetById(int id) => await _repository.GetById(id);

    public async Task Update(User user) => await _repository.Update(user);

    public async Task Delete(int id) => await _repository.Delete(id);
}
