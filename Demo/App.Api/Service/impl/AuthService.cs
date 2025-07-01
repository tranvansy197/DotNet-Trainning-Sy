using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using App.Api.common;
using App.Api.Domains;
using App.Api.exception;
using App.Api.Models;
using App.Api.repository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace App.Api.Service.impl;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();
    private readonly JwtSettings _jwtSettings;
    private readonly IMapper _mapper;
    public AuthService(IUserRepository userRepository, JwtSettings jwtSettings, IMapper mapper)
    {
        _userRepository = userRepository;
        _jwtSettings = jwtSettings;
        _mapper = mapper;
    }
    
    public async Task<string> Login(LoginRequestDTO request)
    {
        var user = await _userRepository.FindByUsername(request.Username);
        if (user == null || _passwordHasher.VerifyHashedPassword(null, user.PasswordHash, request.Password) != PasswordVerificationResult.Success)
            throw new BusinessException("Invalid username or password", HttpStatusCode.Unauthorized);

        var tokenHandler = new JwtSecurityTokenHandler();
        // Ensure the secret key is at least 16 bytes for HmacSha256 and use UTF8 encoding
        var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.Name)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        return jwtToken;
    }

    public async Task Register(RegisterRequestDTO request)
    {
        var user = _mapper.Map<User>(request);

        user.PasswordHash = _passwordHasher.HashPassword(null, request.Password);
        await _userRepository.SaveUser(user); ;
    }
}