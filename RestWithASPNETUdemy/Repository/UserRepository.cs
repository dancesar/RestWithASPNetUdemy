using System.Security.Cryptography;
using System.Text;
using RestWithASPNETUdemy.Data.DTO;
using RestWithASPNETUdemy.model;
using RestWithASPNETUdemy.model.Context;

namespace RestWithASPNETUdemy.Repository;

public class UserRepository : IUserRepository
{
    private readonly MySQLContext _context;

    public UserRepository(MySQLContext context)
    {
        _context = context;
    }

    public User? ValidateCredentials(UserDTO userDto)
    {
        var pass = ComputeHash(userDto.Password, new SHA256CryptoServiceProvider());
        return _context.Users.FirstOrDefault(u => (u.UserName == userDto.UserName) && (u.Password == pass));
    }    
    
    public User? ValidateCredentials(string userName)
    {
        return _context.Users.SingleOrDefault(
            u => (userName == userName));
    }

    public bool RevokeToken(string userName)
    {
        var user = _context.Users.SingleOrDefault(u => (userName == userName));
        if (user is null) return false;
        user.RefreshToken = null;
        _context.SaveChanges();
        return true;
    }        

    public User? RefreshUserInfo(User user)
    {
        if (!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;
        
        var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));
        if (result != null)
        {
            try
            {
                _context.Entry(result).CurrentValues.SetValues(user);
                _context.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        return result;
    }

    private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
    {
        Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
        return BitConverter.ToString(hashedBytes);
    }
}