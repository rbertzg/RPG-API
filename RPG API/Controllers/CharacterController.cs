using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPG_API.Dtos.Character;

namespace RPG_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;

    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    [HttpGet, Authorize]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> GetAll()
    {
        return Ok(await _characterService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> Get(int id)
    {
        return Ok(await _characterService.GetById(id));
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> AddCharacter(AddCharacterRequestDto newCharacter)
    {
        return Ok(await _characterService.AddCharacter(newCharacter));
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter)
    {
        var response = await _characterService.UpdateCharacter(updatedCharacter);
        return response.Data is not null ? Ok(response) : NotFound(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> DeleteCharacter(int id)
    {
        var response = await _characterService.DeleteCharacter(id);
        return response.Data is null ? NotFound(response) : Ok(response);
    }
}
