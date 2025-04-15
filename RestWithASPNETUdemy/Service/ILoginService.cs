using RestWithASPNETUdemy.Data.DTO;

namespace RestWithASPNETUdemy.Business;

public interface ILoginService
{
    TokenDTO ValidateCredentials(UserDTO userDto);
    
    TokenDTO ValidateCredentials(TokenDTO tokenDto);
    
    bool RevokeToken(string userName);
}