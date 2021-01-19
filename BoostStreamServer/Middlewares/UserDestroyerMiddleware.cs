using BoostStreamServer.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace BoostStreamServer.Middlewares
{
    public class UserDestroyerMiddleware
    {
        private readonly RequestDelegate _next;

        public UserDestroyerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            if (!string.IsNullOrEmpty(httpContext.User.Identity.Name))
            {
                var user = await userManager.FindByNameAsync(httpContext.User.Identity.Name);

                if (user is not null && user.Banned)
                {
                    //Log the user out and redirect back to homepage
                    await signInManager.SignOutAsync();
                    httpContext.Response.Redirect("/");
                }
            }
            await _next(httpContext);
        }
    }
}
