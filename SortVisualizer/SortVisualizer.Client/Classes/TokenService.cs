using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

public class TokenService
{
    private const string SecretKey = "ваш_секретный_ключ"; // Замените на свой секретный ключ
    private readonly SymmetricSecurityKey _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

    // Метод для генерации токена
    public string GenerateToken()
    {
        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "ваш_сервер",
            audience: "ваш_клиент",
            expires: DateTime.UtcNow.AddMinutes(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool IsTokenExpired(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            return true;
        }

        var expirationDateUnix = jwtToken.Payload.Exp;
        var expirationDate = DateTimeOffset.FromUnixTimeSeconds((long)expirationDateUnix).UtcDateTime;

        return DateTime.UtcNow >= expirationDate;
    }

    // Метод для валидации токена
    public bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _key,
                ValidateIssuer = true,
                ValidIssuer = "ваш_сервер",
                ValidateAudience = true,
                ValidAudience = "ваш_клиент",
                ValidateLifetime = true
            }, out _);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
