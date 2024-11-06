using DafTask.Dtos;
using DafTask.Models;
using DafTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DafTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserManager<UserProfile> userManager;
        private readonly SignInManager<UserProfile> signInManager;
        private readonly IAuthenticationService authenticationService;

        public UserProfileController(UserManager<UserProfile> userManager, SignInManager<UserProfile> signInManager,
            IAuthenticationService authenticationService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto>> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if(user is null)
                return Unauthorized(new ResponseDto
                {
                    StatusCode = 401,
                    Message = "Unauthorized User no such email",
                   
                });

            var result= await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if(!result.Succeeded)
                return Unauthorized(new ResponseDto
                {
                    StatusCode = 401,
                    Message = "Unauthorized User",                     
                });
            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(authenticationService.CreateJwtToken(user));
            //Response.Cookies.Append("token", token);
            return new ResponseDto
            {
                StatusCode = 200,
                Message = "User successfully authorized",
                Data = new AuthDto
                {
                    Email = user.Email!,
                    Token = token,
                    UserName = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                }
            };

        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseDto>> Register(RegisterDto registerDto)
        {
            var user = await userManager.FindByEmailAsync(registerDto.Email);
            if (user is not null) 
                return BadRequest(new ResponseDto
                {
                    StatusCode = 400,
                    Message = "Registered email",
                });

            var newUser = new UserProfile
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                DateOfBirth = registerDto.DateOfBirth,
                Email = registerDto.Email,
                UserName = registerDto.Email,
            };
            var result = await userManager.CreateAsync(newUser,registerDto.Password);
   
            if(!result.Succeeded)
                return BadRequest(new ResponseDto
                {
                    StatusCode = 400,
                    Message = "Error occured while registering user!",
                });
            var handler = new JwtSecurityTokenHandler();
            return new ResponseDto
            {
                StatusCode = 200,
                Message = "User successfully authorized",
                Data = new AuthDto
                {
                    Email = newUser.Email,
                    Token = handler.WriteToken(authenticationService.CreateJwtToken(newUser)),
                    UserName = newUser.UserName,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    DateOfBirth=newUser.DateOfBirth,
                }
            };
        }


        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult<ResponseDto>> UpdateUserProfile(UpdateUserDto updateDto)
        {
            var id = User.Claims.Single(u=> u.Type=="uid");
            var user = await userManager.FindByIdAsync(id.Value);
            if (user is null)
            {
                return Unauthorized(new ResponseDto
                {
                    StatusCode = 401,
                    Message = $"Unauthorized: No such email",
                });
            }

            if(updateDto.Password is not null && updateDto.OldPassword is not null)
            {
                var result = await signInManager.CheckPasswordSignInAsync(user, updateDto.OldPassword, false);
                if (!result.Succeeded)
                    return Unauthorized(new ResponseDto
                    {
                        StatusCode = 401,
                        Message = "Unauthorized User: wrong password",
                    });
            }

            // Update email if different
            await authenticationService.UpdateEmail(user, updateDto);

            // Update password if provided and different from current
            await authenticationService.UpdatePassword(user, updateDto);

            // Update additional profile details
            if(!string.IsNullOrEmpty(updateDto.FirstName))
                user.FirstName = updateDto.FirstName;
            if (!string.IsNullOrEmpty(updateDto.LastName))
                user.LastName = updateDto.LastName;
            if(!(updateDto.DateOfBirth==DateTime.Now))
                user.DateOfBirth = updateDto.DateOfBirth;

            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return BadRequest(new ResponseDto
                {
                    StatusCode = 400,
                    Message = $"Failed to update profile: {string.Join(", ", updateResult.Errors.Select(e => e.Description))}"
                });
            }
            // Generate new token and response
            var handler = new JwtSecurityTokenHandler();
            return new ResponseDto
            {
                StatusCode = 200,
                Message = "User successfully updated",
                Data = new AuthDto
                {
                    Email = user.Email,
                    Token = handler.WriteToken(authenticationService.CreateJwtToken(user)),
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                }
            };
        }


        [Authorize]
        [HttpDelete("delete")]
        public async Task<ActionResult<ResponseDto>> DeleteProfile()
        {
            var id = User.Claims.SingleOrDefault(u => u.Type == "uid");
            var user = await userManager.FindByIdAsync(id!.Value);
            if (user is null)
            {
                return Unauthorized(new ResponseDto
                {
                    StatusCode = 401,
                    Message = $"Unauthorized user",
                });
            }

            
            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return Unauthorized(new ResponseDto
                {
                    StatusCode = 401,
                    Message = $"Unauthorized user",
                });
            }

            return Ok("Profile deleted");

        }

    }
}

