using DafTask.Dtos;
using DafTask.Helpers;
using DafTask.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DafTask.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<UserProfile> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        public AuthenticationService(UserManager<UserProfile> userManager, IOptions<JWT> jwt, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }
        

        public JwtSecurityToken CreateJwtToken(UserProfile user)
        {
            var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid",user.Id),
                new Claim (JwtRegisteredClaimNames.Name, user.UserName),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new
                (
                    issuer: _jwt.Issuer,
                    audience: _jwt.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(_jwt.Duration),
                    signingCredentials: signInCredentials
                );
            return jwtSecurityToken;
        }

        public async Task UpdateEmail(UserProfile user, UpdateUserDto updateDto)
        {
            if (!string.IsNullOrEmpty(updateDto.NewEmail))
            {
                user.Email = updateDto.NewEmail;
                user.UserName = updateDto.NewEmail; // Ensure username is updated if using email as username
                var emailUpdateResult = await _userManager.UpdateAsync(user);
                
            }
        }

        public async Task UpdatePassword(UserProfile user, UpdateUserDto updateUserDto)
        {
            if (!string.IsNullOrEmpty(updateUserDto.Password) && !string.IsNullOrEmpty(updateUserDto.OldPassword))
            {
                var passwordUpdateResult = await _userManager.ChangePasswordAsync(user, updateUserDto.OldPassword, 
                    updateUserDto.Password);
                await _userManager.UpdateAsync(user);

            }
        }
    }
}
