namespace JAValmet_Backend.Core.Services.JWT;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using JAValmet_Backend.Domain.Services;
using JAValmet_Backend.Domain.Models;
using JAValmet_Backend.Core.Middlewares.ErrorHandler;
using System.Net;
using Microsoft.AspNetCore.Identity;

public class JwtService : IJwtService
{
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly SymmetricSecurityKey _securityKey;
    private readonly SigningCredentials _credentials;
    private UserContext _userContext;

    public JwtService(
        JwtSecurityTokenHandler tokenHandler,
        UserContext userContext,
        JwtSettings settings)
    {
        _tokenHandler = tokenHandler;
        _userContext = userContext;

        _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey));
        _credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha512);
    }

    public OutboundToken GenerateToken(UserDTO user)
    {
        var claims = new List<Claim>
        {
            new("UserId", user.Id.ToString()),
        };

        var SecToken = new JwtSecurityToken(
            "JAValmet",
            audience: null,
            claims: claims,
            expires: DateTime.Now.AddMonths(3),
            signingCredentials: _credentials);

        var token = _tokenHandler.WriteToken(SecToken);

        return new OutboundToken(token);
    }

    public void ValidateToken(string jwt)
    {
        ClaimsPrincipal claims;

        try
        {
            claims = _tokenHandler.ValidateToken(jwt,
                    new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        ValidIssuer = "JAValmet",
                        IssuerSigningKey = _securityKey
                    },
                    out var validatedToken);
        }
        catch (SecurityTokenException)
        {
            throw new AppException("Invalid token format or signature.", HttpStatusCode.Unauthorized);
        }
        catch (Exception)
        {
            throw new AppException("Unable to validate token and its claims.", HttpStatusCode.InternalServerError);
        }

        var userId = claims.FindFirst("UserId")?.Value ?? throw new AppException("Token is missing necessary claims.", HttpStatusCode.BadRequest);

        if (!Guid.TryParse(userId, out var parsedUserId))
            throw new AppException("Token contains an invalid UserId claim.", HttpStatusCode.BadRequest);

        _userContext.Fill(new ContextData
        {
            UserId = parsedUserId
        });
    }

}
