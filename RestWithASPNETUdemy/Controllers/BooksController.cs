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
    private IBooksBusiness _booksBusiness;

    public BooksController(ILogger<BooksController> logger, IBooksBusiness booksBusiness)
    {
        _logger = logger;
        _booksBusiness = booksBusiness;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_booksBusiness.FindAll());
    }

    [HttpGet("{id}")]
    public IActionResult Get(long id)
    {
        var books = _booksBusiness.FindById(id);
        if (books == null) return NotFound();
        return Ok(_booksBusiness.FindById(id));
    }

    [HttpPost]
    public IActionResult Post([FromBody] Books books)
    {
        if (books == null) return BadRequest();
        return Ok(_booksBusiness.Create(books));
    }

    [HttpPut("{id}")]
    public IActionResult Put(long id, [FromBody] Books books)
    {
        if (books == null) return BadRequest();
        return Ok(_booksBusiness.Update(books));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        _booksBusiness.Delete(id);
        return NoContent();
    }
}