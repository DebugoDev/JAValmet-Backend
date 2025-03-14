namespace JAValmet_Backend.Domain.Services;

using JAValmet_Backend.Domain.Models;

public interface IJwtService
{
    public OutboundToken GenerateToken(UserDTO user);
    public void ValidateToken(string jwt);
}