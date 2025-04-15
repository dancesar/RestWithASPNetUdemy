using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using RestWithASPNETUdemy.Configurations;
using RestWithASPNETUdemy.Data.DTO;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.ServicesToken;

namespace RestWithASPNETUdemy.Business.Implementations;

public class LoginServiceImplementation : ILoginService
{
    private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
    private TokenConfiguration _configuration;
    
    private IUserRepository _repository;
    private readonly ITokenService _tokenService;

    public LoginServiceImplementation(TokenConfiguration configuration, IUserRepository repository, ITokenService tokenService)
    {
        _configuration = configuration;
        _repository = repository;
        _tokenService = tokenService;   
    }

    public TokenDTO ValidateCredentials(UserDTO userCredentials)
    {
        var user = _repository.ValidateCredentials(userCredentials);
        if (user == null) return null;
        var clais = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
        };
        
        var accessToken = _tokenService.GenerateAccessToken(clais);
        var refreshToken = _tokenService.GenerateRefreshToken();
        
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);

        _repository.RefreshUserInfo(user);
        
        DateTime createDate = DateTime.Now;
        DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

        return new TokenDTO(
            true,
            createDate.ToString(DATE_FORMAT),
            expirationDate.ToString(DATE_FORMAT),
            accessToken,
            refreshToken
            );
    }

    public TokenDTO ValidateCredentials(TokenDTO tokenDto)
    {
        var accessToken = tokenDto.AccessToken;
        var refreshToken = tokenDto.RefreshToken;
        
        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        
        var username = principal.Identity.Name;
        
        var user = _repository.ValidateCredentials(username);

        if (user == null || 
            user.RefreshToken != refreshToken || 
            user.RefreshTokenExpiryTime < DateTime.Now) return null;
        
        accessToken = _tokenService.GenerateAccessToken(principal.Claims);
        refreshToken = _tokenService.GenerateRefreshToken();
        
        user.RefreshToken = refreshToken;
        
        DateTime createDate = DateTime.Now;
        DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

        return new TokenDTO(
            true,
            createDate.ToString(DATE_FORMAT),
            expirationDate.ToString(DATE_FORMAT),
            accessToken,
            refreshToken
        );
    }

    public bool RevokeToken(string userName)
    {
        return _repository.RevokeToken(userName);
    }
}