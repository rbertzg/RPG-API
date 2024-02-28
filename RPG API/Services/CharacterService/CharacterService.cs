using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RPG_API.Data;
using RPG_API.Dtos.Character;

namespace RPG_API.Services.CharacterService;

public class CharacterService : ICharacterService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public CharacterService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResponse<List<GetCharacterResponseDto>>> AddCharacter(AddCharacterRequestDto newCharacter)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();

        var character = (_mapper.Map<Character>(newCharacter));

        _context.Characters.Add(character);
        await _context.SaveChangesAsync();

        var dbCharacters = await _context.Characters.ToListAsync();
        serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToList();

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAll()
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();

        var dbCharacters = await _context.Characters.ToListAsync();
        serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToList();

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterResponseDto>> GetById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();

        var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
        serviceResponse.Data = _mapper.Map<GetCharacterResponseDto>(dbCharacter);

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter)
    {
        var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();
        try
        {
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

            if (dbCharacter is null) throw new Exception($"User with Id {updatedCharacter.Id} not found");

            _mapper.Map(updatedCharacter, dbCharacter);

            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<GetCharacterResponseDto>(dbCharacter);
        }
        catch (Exception exception)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = exception.Message;
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterResponseDto>>> DeleteCharacter(int id)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();
        try
        {
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

            if (dbCharacter is null) throw new Exception("User not found");

            _context.Characters.Remove(dbCharacter);
            await _context.SaveChangesAsync();

            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToList();
        }
        catch (Exception exception)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = exception.Message;

        }
        return serviceResponse;
    }
}
