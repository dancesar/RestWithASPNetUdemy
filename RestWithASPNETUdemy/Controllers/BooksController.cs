using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.model;

namespace RestWithASPNETUdemy.Controllers;

[ApiVersion("1")]
[ApiController]
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
    public IActionResult Get()
    {
        return Ok(_booksService.FindAll());
    }

    [HttpGet("{id}")]
    public IActionResult Get(long id)
    {
        var books = _booksService.FindById(id);
        if (books == null) return NotFound();
        return Ok(_booksService.FindById(id));
    }

    [HttpPost]
    public IActionResult Post([FromBody] Books books)
    {
        if (books == null) return BadRequest();
        return Ok(_booksService.Create(books));
    }

    [HttpPut("{id}")]
    public IActionResult Put(long id, [FromBody] Books books)
    {
        if (books == null) return BadRequest();
        return Ok(_booksService.Update(books));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        _booksService.Delete(id);
        return NoContent();
    }
}