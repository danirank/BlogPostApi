using BlogPostApi.Core.Interfaces;
using BlogPostApi.Data.DTO;
using BlogPostApi.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogPostApi.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;
        public AuthService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }



        public async Task<ServiceResult<LoginResponseDto>> LoginAsync(LoginUserDto dto)
        {
            var userEntity = dto.UserNameOrEmail.Contains("@") ?
                await _userManager.FindByEmailAsync(dto.UserNameOrEmail)
                : await _userManager.FindByNameAsync(dto.UserNameOrEmail);

            if (userEntity is null)
                return ServiceResult<LoginResponseDto>.Fail(new List<string> { "User not found" });

            var correctPassword = await _userManager.CheckPasswordAsync(userEntity, dto.PassWord);

            if (!correctPassword)
                return ServiceResult<LoginResponseDto>.Fail(new List<string> { "Wrong password" });


            var token = await GenerateToken(userEntity);

            return ServiceResult<LoginResponseDto>.Ok(new LoginResponseDto
            {
                Token = token
            });

        }

        public async Task<string> GenerateToken(AppUser user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName ?? ""),
            new Claim(ClaimTypes.Email, user.Email ?? "")
        };



            var secretKey = _config["JwtSettings:Key"];
            var issuer = _config["JwtSettings:Issuer"];
            var audience = _config["JwtSettings:Audience"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}

