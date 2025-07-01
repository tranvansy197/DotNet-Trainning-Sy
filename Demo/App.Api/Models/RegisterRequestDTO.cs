namespace App.Api.Models;

public class RegisterRequestDTO
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public long RoleId { get; set; }
}