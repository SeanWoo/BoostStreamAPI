using Microsoft.AspNetCore.Builder;

namespace BoostStreamServer.Middlewares
{
    public static class MiddlewareHelper
    {
        public static IApplicationBuilder UseUserDestroyer(this IApplicationBuilder app)
            => app.UseMiddleware<UserDestroyerMiddleware>();
    }
}
