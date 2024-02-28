﻿namespace RPG_API.Dtos.Character;

public class AddCharacterRequestDto
{
    public required string Name { get; set; }
    public int HitPoints { get; set; } = 100;
    public int Strength { get; set; } = 10;
    public int Defense { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public RpgClass Class { get; set; } = RpgClass.Knight;
}
