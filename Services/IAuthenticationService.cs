using DafTask.Dtos;
using DafTask.Models;
using System.IdentityModel.Tokens.Jwt;

namespace DafTask.Services
{
    public interface IAuthenticationService
    {
        JwtSecurityToken CreateJwtToken(UserProfile userProfile);
        Task UpdateEmail (UserProfile user, UpdateUserDto updateDto);
        Task UpdatePassword(UserProfile user, UpdateUserDto updateUserDto);
    }
}
