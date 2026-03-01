using PersonApi.DTOs;
using PersonApi.Models;

namespace PersonApi.Services;

public interface IPersonService
{
    Task<IEnumerable<Person>> GetAllPersonsAsync();
    Task<PagedResult<Person>> GetPersonsPagedAsync(int page, int pageSize);
    Task<Person?> GetPersonByIdAsync(Guid id);
    Task<Person> CreatePersonAsync(Person person);
}
