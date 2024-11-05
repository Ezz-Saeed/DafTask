﻿using DafTask.Dtos;
using DafTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DafTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserManager<UserProfile> userManager;
        private readonly SignInManager<UserProfile> signInManager;

        public UserProfileController(UserManager<UserProfile> userManager, SignInManager<UserProfile> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto>> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if(user is null)
                return Unauthorized(new ResponseDto
                {
                    StatusCode = 401,
                    Message = "Unauthorized User",
                   
                });

            var result= await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if(!result.Succeeded)
                return Unauthorized(new ResponseDto
                {
                    StatusCode = 401,
                    Message = "Unauthorized User",                     
                });
            return new ResponseDto
            {
                StatusCode = 200,
                Message = "User successfully authorized",
                Data = new AuthDto
                {
                    Email = user.Email!,
                    Toke = "Token"
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
            return new ResponseDto
            {
                StatusCode = 200,
                Message = "User successfully authorized",
                Data = new AuthDto
                {
                    Email = newUser.Email,
                    Toke = "Token"
                }
            };
        }
    }
}
