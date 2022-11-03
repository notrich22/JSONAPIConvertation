namespace JSONConvertationAPI
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate next;
        private readonly string customToken = "randomToken";
        public TokenMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var logger = context.RequestServices.GetService<ILogger<Program>>();
            var token = context.Request.Headers["token"];
            if (token == customToken)
            {
                logger.LogInformation($"Token accepted; access granted: {DateTime.Now}");
                await next.Invoke(context);
            }
            else
            {
                logger.LogError($"Wrong token; access denied:{DateTime.Now}");
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync($"Invalid token: {DateTime.Now}");
            }
        }
    }
}
