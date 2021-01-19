using BoostStreamServer.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace BoostStreamServer.Data
{
    public static class ApplicationDbInitializer
    {
        public static async Task SeedUsers(UserManager<User> userManager)
        {
            if (await userManager.FindByNameAsync("SeanWoo") == null)
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "SeanWoo",
                    Token = new() { Id = Guid.NewGuid(), Access = "ALL" },
                    RegistrationAt = DateTime.Now
                };

                var result = await userManager.CreateAsync(user, "123123123");//свой пароль прятал :D

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
