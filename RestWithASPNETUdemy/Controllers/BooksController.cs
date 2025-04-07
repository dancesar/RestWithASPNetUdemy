using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Data.DTO;
using RestWithASPNETUdemy.Hypermedia.Filters;

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
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Get()
    {
        return Ok(_booksService.FindAll());
    }

    [HttpGet("{id}")]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Get(long id)
    {
        var books = _booksService.FindById(id);
        if (books == null) return NotFound();
        return Ok(_booksService.FindById(id));
    }

    [HttpPost]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Post([FromBody] BooksDTO booksDto)
    {
        if (booksDto == null) return BadRequest();
        return Ok(_booksService.Create(booksDto));
    }

    [HttpPut]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Put(long id, [FromBody] BooksDTO booksDto)
    {
        if (booksDto == null) return BadRequest();
        return Ok(_booksService.Update(booksDto));
    }

    [HttpDelete("{id}")]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Delete(long id)
    {
        _booksService.Delete(id);
        return NoContent();
    }
}