using Microsoft.AspNetCore.Mvc;
using PersonApi.DTOs;
using PersonApi.Models;
using PersonApi.Services;

namespace PersonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IPersonService _service;

    public PersonsController(IPersonService service)
    {
        _service = service;
    }

    // GET /api/persons?page=1&pageSize=10
    [HttpGet]
    public async Task<ActionResult<PagedResult<Person>>> GetAllPersons(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _service.GetPersonsPagedAsync(page, pageSize);
        return Ok(result);
    }

    // GET /api/persons/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> GetPersonById(Guid id)
    {
        var person = await _service.GetPersonByIdAsync(id);
        if (person == null) return NotFound();
        return Ok(person);
    }

    // POST /api/persons
    [HttpPost]
    public async Task<ActionResult<Person>> CreatePerson(Person person)
    {
        var createdPerson = await _service.CreatePersonAsync(person);
        return CreatedAtAction(nameof(GetPersonById), new { id = createdPerson.Id }, createdPerson);
    }
}
