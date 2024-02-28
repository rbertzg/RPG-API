using RPG_API.Dtos.Character;

namespace RPG_API.Services.CharacterService;

public interface ICharacterService
{
    Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAll();
    Task<ServiceResponse<GetCharacterResponseDto>> GetById(int id);
    Task<ServiceResponse<List<GetCharacterResponseDto>>> AddCharacter(AddCharacterRequestDto newCharacter);
    Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter);
    Task<ServiceResponse<List<GetCharacterResponseDto>>> DeleteCharacter(int id);

}
