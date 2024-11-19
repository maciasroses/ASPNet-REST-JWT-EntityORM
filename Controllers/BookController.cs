using AutoMapper;
using api_rest_cs.DTOs;
using api_rest_cs.Models;
using api_rest_cs.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace api_rest_cs.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BookController(BookService service, IMapper mapper) : ControllerBase
{
    private readonly BookService _service = service;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var books = await _service.GetAll();
        var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);
        return Ok(booksDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var book = await _service.GetById(id);
        if (book == null) return NotFound();
        var bookDto = _mapper.Map<BookDto>(book);
        return Ok(bookDto);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateBookDto createProductDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var book = _mapper.Map<Book>(createProductDto);
        await _service.Add(book);

        var bookDto = _mapper.Map<BookDto>(book);
        return CreatedAtAction(nameof(GetById), new { id = bookDto.Id }, bookDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateBookDto updateBookDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var book = await _service.GetById(id);
        if (book == null) return NotFound();

        _mapper.Map(updateBookDto, book);
        await _service.Update(book);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var book = await _service.GetById(id);
        if (book == null) return NotFound();

        await _service.Delete(id);

        return NoContent();
    }
}
