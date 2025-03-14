namespace JAValmet_Backend.Core.Services.JWT;

public record JwtSettings
{
    public required string SecretKey { get; init; }
}