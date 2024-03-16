namespace AuthUsage.Middlewares
{
    public class TokenExtractionMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenExtractionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("Authorization",out var authHeader))
            {
                string token = authHeader.ToString();
                context.Items["Token"] = token;
            }

            await _next(context);
        }
    }
}
