namespace SurveyMonkey.WebApi.Middlewares
{
    public class ResponseBodyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseBodyMiddleware> _logger;

        public ResponseBodyMiddleware(RequestDelegate next, ILogger<ResponseBodyMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBody = context.Response.Body;

            var memStream = new MemoryStream(); 

            context.Response.Body = memStream;

            await _next(context);

            memStream.Seek(0, SeekOrigin.Begin);

            var content =await  new StreamReader(memStream).ReadToEndAsync();

            memStream.Seek(0, SeekOrigin.Begin);

            _logger.LogInformation(content);

            await memStream.CopyToAsync(originalBody);

            context.Response.Body = originalBody;   

        }

    }
}
