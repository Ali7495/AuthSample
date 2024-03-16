using AuthUsage.Middlewares;

namespace AuthUsage.Extentions
{
    public static class TokenExtention
    {
        public static IApplicationBuilder UseTokenExtraction(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TokenExtractionMiddleware>();
        }
    }
}
