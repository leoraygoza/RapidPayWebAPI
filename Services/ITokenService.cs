namespace RapidPayWebAPI.Services;

public interface ITokenService
{
    string GenerateToken(string username);
}