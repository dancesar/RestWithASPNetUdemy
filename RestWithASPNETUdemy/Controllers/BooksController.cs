using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Data.DTO;
using RestWithASPNETUdemy.Hypermedia.Filters;

namespace RestWithASPNETUdemy.Controllers;

[ApiVersion("1")]
[ApiController]
[Authorize("Bearer")]
[Route("api/[controller]/v{version:apiVersion}")]
public class BooksController : ControllerBase
{
    private readonly ILogger<BooksController> _logger;
    private IBooksService _booksService;

    public BooksController(ILogger<BooksController> logger, IBooksService booksService)
    {
        _logger = logger;
        _booksService = booksService;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<PersonDTO>))]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Get()
    {
        return Ok(_booksService.FindAll());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(PersonDTO))]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Get(long id)
    {
        var books = _booksService.FindById(id);
        if (books == null) return NotFound();
        return Ok(_booksService.FindById(id));
    }

    [HttpPost]
    [ProducesResponseType(200, Type = typeof(PersonDTO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Post([FromBody] BooksDTO booksDto)
    {
        if (booksDto == null) return BadRequest();
        return Ok(_booksService.Create(booksDto));
    }

    [HttpPut]
    [ProducesResponseType(200, Type = typeof(PersonDTO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Put(long id, [FromBody] BooksDTO booksDto)
    {
        if (booksDto == null) return BadRequest();
        return Ok(_booksService.Update(booksDto));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Delete(long id)
    {
        _booksService.Delete(id);
        return NoContent();
    }
}