using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using KePass.Server.Commons;
using KePass.Server.Services.Definitions;
using Microsoft.IdentityModel.Tokens;

namespace KePass.Server.Services.Implementations;

public class TokenService(IEnvironmentService environment, ILogger<TokenService> logger) : ITokenService
{
    public static byte[] GetSecretKey(IEnvironmentService environment)
    {
        return SHA256.HashData(Encoding.UTF8.GetBytes(environment.Get("APPLICATION_KEY") ??
                                                      throw new Exception("Application Key not set")));
    }

    public string? Create(CurrentAccount? current)
    {
        if (current == null) return null;

        try
        {
            var key = GetSecretKey(environment);
            var handler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(current.ToClaims()),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256),
            };

            var token = handler.CreateToken(descriptor);

            return handler.WriteToken(token);
        }
        catch (Exception e)
        {
            logger.LogTrace(e.ToString());
            return null;
        }
    }

    public CurrentAccount? Parse(string? token)
    {
        if (string.IsNullOrEmpty(token)) return null;

        try
        {
            var key = GetSecretKey(environment);
            var handler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };
            
            var principal = handler.ValidateToken(token, validationParameters, out _);

            return CurrentAccount.CreateFromClaims(principal.Claims.ToArray());
        }
        catch (Exception e)
        {
            logger.LogTrace(e.ToString());
            return null;
        }
    }
}