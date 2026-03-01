using PersonApi.DTOs;
using PersonApi.Models;
using PersonApi.Repositories;

namespace PersonApi.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _repository;
    private readonly ILogger<PersonService> _logger;

    public PersonService(IPersonRepository repository, ILogger<PersonService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<Person>> GetAllPersonsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<PagedResult<Person>> GetPersonsPagedAsync(int page, int pageSize)
    {
        return await _repository.GetPagedAsync(page, pageSize);
    }

    public async Task<Person?> GetPersonByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Person> CreatePersonAsync(Person person)
    {
        if (person.DateOfBirth.Date > DateTime.Today)
            throw new ArgumentException("วันเกิดไม่สามารถเป็นวันในอนาคตได้");

        person.Id = Guid.NewGuid();
        person.CreatedAt = DateTime.UtcNow;

        var result = await _repository.AddAsync(person);
        _logger.LogInformation("Created person {Id} ({FirstName} {LastName})", result.Id, result.FirstName, result.LastName);
        return result;
    }
}
