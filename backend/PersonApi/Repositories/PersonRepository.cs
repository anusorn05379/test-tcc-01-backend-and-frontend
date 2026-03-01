using Microsoft.EntityFrameworkCore;
using PersonApi.Data;
using PersonApi.DTOs;
using PersonApi.Models;

namespace PersonApi.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly AppDbContext _context;

    public PersonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _context.Persons.OrderByDescending(p => p.CreatedAt).ToListAsync();
    }

    public async Task<PagedResult<Person>> GetPagedAsync(int page, int pageSize)
    {
        var query = _context.Persons.OrderByDescending(p => p.CreatedAt);

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Person> { Items = items, TotalCount = totalCount };
    }

    public async Task<Person?> GetByIdAsync(Guid id)
    {
        return await _context.Persons.FindAsync(id);
    }

    public async Task<Person> AddAsync(Person person)
    {
        _context.Persons.Add(person);
        await _context.SaveChangesAsync();
        return person;
    }
}
