using App.Api.Models;

namespace App.Api.Service;

public interface IAuthService
{
    Task<string> Login(LoginRequestDTO request);
    Task Register(RegisterRequestDTO request);
}