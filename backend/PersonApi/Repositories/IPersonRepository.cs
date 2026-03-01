using PersonApi.DTOs;
using PersonApi.Models;

namespace PersonApi.Repositories;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetAllAsync();
    Task<PagedResult<Person>> GetPagedAsync(int page, int pageSize);
    Task<Person?> GetByIdAsync(Guid id);
    Task<Person> AddAsync(Person person);
}
