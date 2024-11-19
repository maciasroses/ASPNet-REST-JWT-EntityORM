using AutoMapper;
using api_rest_cs.DTOs;
// using api_rest_cs.Models;
using api_rest_cs.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace api_rest_cs.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController(UserService service, IMapper mapper) : ControllerBase
{
    private readonly UserService _service = service;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var users = await _service.GetAll();
        var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
        return Ok(usersDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var user = await _service.GetById(id);
        if (user == null) return NotFound();
        var userDto = _mapper.Map<UserDto>(user);
        return Ok(userDto);
    }
}
