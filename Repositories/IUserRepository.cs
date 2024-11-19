using api_rest_cs.Models;

namespace api_rest_cs.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll();
    Task<User?> GetById(int id);
    Task Update(User user);
    Task Delete(int id);
}
