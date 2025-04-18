using System.Security.Claims;

namespace RestWithASPNETUdemy.ServicesToken;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    
    string GenerateRefreshToken();
    
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}