using RestWithASPNETUdemy.Data.DTO;
using RestWithASPNETUdemy.model;

namespace RestWithASPNETUdemy.Repository;

public interface IUserRepository
{
    User? ValidateCredentials(UserDTO userDto);
    
    User? ValidateCredentials(string userName);

    bool RevokeToken(string userName);
    
    User? RefreshUserInfo(User user);
}