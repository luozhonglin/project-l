

namespace Project.L.Common
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        private const string API_KEY_HEADER = "X-API-KEY";
        private readonly IConfiguration _config;

        public RequestMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            var allowPath = _config.GetSection("AllowRequestURL").Get<List<string>>();
            var isAllow = allowPath?.FirstOrDefault(n=>n==context.Request.Path);
            if (isAllow == null)
            {
                if (!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var extractedApiKey))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("API Key缺失");
                    return;
                }

                var appSettings = context.RequestServices
                    .GetRequiredService<IConfiguration>();
                var apiKey = appSettings.GetValue<string>("ApiKey");

                if (!apiKey.Equals(extractedApiKey))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("无效的API Key");
                    return;
                }
            }
            await _next(context);
        }
    }
}
