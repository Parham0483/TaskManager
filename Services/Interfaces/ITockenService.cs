using TaskManager.Models;

public interface ITokenService
{
    string CreateToken(User user);
}

