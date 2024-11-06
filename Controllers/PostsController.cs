using DafTask.Data;
using DafTask.Dtos;
using DafTask.Dtos.PostDto;
using DafTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DafTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly UserManager<UserProfile> userManager;

        public PostsController(AppDbContext context,UserManager<UserProfile> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpGet("getPosts")]
        public async Task<ActionResult<List<Post>>> GetPosts()
        {
            var id = User.Claims.Single(u => u.Type == "uid");
            var user = await userManager.FindByIdAsync(id.Value);
            if (user is null)
            {
                return Unauthorized(new ResponseDto
                {
                    StatusCode = 401,
                    Message = $"Unauthorized user",
                });
            }

            var posts = await context.Posts.Where(p=>p.UserId == id.Value).ToListAsync();
            return posts;
        }


        [Authorize]
        [HttpPost("addPost")]
        public async Task<ActionResult<AddPostDto>> AddPost(AddPostDto addPostDto)
        {
            var id = User.Claims.Single(u => u.Type == "uid");
            var user = await userManager.FindByIdAsync(id.Value);
            if (user is null)
            {
                return Unauthorized(new ResponseDto
                {
                    StatusCode = 401,
                    Message = $"Unauthorized user",
                });
            }
            Post post = new ()
            {
                Title=addPostDto.Title,
                Content=addPostDto.Content,
                DatePosted=addPostDto.DatePosted,
                UserId=user.Id,
            };

            user.Posts!.Add(post);
            await userManager.UpdateAsync(user);

            return addPostDto;
        }

        [Authorize]
        [HttpPut("updatePost")]
        public async Task<ActionResult<AddPostDto>> UpdatePost(AddPostDto addPostDto, int id)
        {
            var userId = User.Claims.Single(u => u.Type == "uid");
            var user = await userManager.FindByIdAsync(userId.Value);
            if (user is null)
            {
                return Unauthorized(new ResponseDto
                {
                    StatusCode = 401,
                    Message = $"Unauthorized user",
                });
            }

            var post = await context.Posts.FindAsync(id);
            if (post is null)
            {
                return BadRequest(new ResponseDto
                {
                    StatusCode = 400,
                    Message = $"No such post",
                });
            }

            post.Title = addPostDto.Title;
            post.Content = addPostDto.Content;
            post.DatePosted = addPostDto.DatePosted;
            context.Update(post);
            await context.SaveChangesAsync();
            return addPostDto;
        }

        [Authorize]
        [HttpDelete("deletePost")]
        public async Task<ActionResult<Post>> DeletePost(int id)
        {
            var userId = User.Claims.Single(u => u.Type == "uid");
            var user = await userManager.FindByIdAsync(userId.Value);
            if (user is null)
            {
                return Unauthorized(new ResponseDto
                {
                    StatusCode = 401,
                    Message = $"Unauthorized user",
                });
            }

            var post = await context.Posts.FindAsync(id);
            if (post is null)
            {
                return BadRequest(new ResponseDto
                {
                    StatusCode = 400,
                    Message = $"No such post",
                });
            }
            context.Posts.Remove(post);
            await context.SaveChangesAsync();
            return post;
            
        }
    }
}
