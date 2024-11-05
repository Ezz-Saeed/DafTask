using DafTask.Data;
using DafTask.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DafTask.Seeding
{
    public class UserProfileSeed
    {
        public static async Task SeedUsersAsync(UserManager<UserProfile> userManager, AppDbContext context)
        {
            if (!userManager.Users.Any())
            {
                var userProfile = new UserProfile
                {
                    UserName = "user@test.com", // Ensure UserName is set
                    Email = "user@test.com",
                    FirstName = "User",
                    LastName = "User",
                    DateOfBirth = DateTime.Now.Date
                };

                var result = await userManager.CreateAsync(userProfile, "Ab$$1234");
                if (result.Succeeded)
                {
                    var posts = new List<Post>
                    {
                        new Post
                        {
                            Title = "Post 1",
                            Content = "post1 content",
                            DatePosted = DateTime.Now,
                            UserId = userProfile.Id // Associate with the created user
                        }
                    };

                    await context.Posts.AddRangeAsync(posts);
                    await context.SaveChangesAsync();
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Failed to create user: {errors}");
                }
            }
        }
    }
}
