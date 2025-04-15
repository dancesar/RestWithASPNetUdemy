using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RestWithASPNETUdemy.Configurations;

namespace RestWithASPNETUdemy.ServicesToken.Implementations;

public class TokenService : ITokenService
{
    private TokenConfiguration _confoguration;

    public TokenService(TokenConfiguration confoguration)
    {
        _confoguration = confoguration;
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_confoguration.Secret));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var options = new JwtSecurityToken(
            issuer: _confoguration.Issuer,
            audience: _confoguration.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_confoguration.Minutes),
            signingCredentials: signingCredentials
        );

        string tokenString = new JwtSecurityTokenHandler().WriteToken(options);
        return tokenString;
    }

    public string GenerateRefreshToken()
    {
        var randonNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randonNumber);
            return Convert.ToBase64String(randonNumber);
        }
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameter = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_confoguration.Secret)),
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameter, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture)) throw new SecurityTokenException("Invalid token");

        return principal;
    }
}