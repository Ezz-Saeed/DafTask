using DafTask.Dtos;
using DafTask.Models;
using System.IdentityModel.Tokens.Jwt;

namespace DafTask.Services
{
    public interface IAuthenticationService
    {
        JwtSecurityToken CreateJwtToken(UserProfile userProfile);
        Task<ResponseDto> UpdateEmail (string email, string newEmail);
    }
}
