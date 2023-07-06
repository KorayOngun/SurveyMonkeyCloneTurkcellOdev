using Microsoft.AspNetCore.Http.Extensions;
using System.Diagnostics;

namespace SurveyMonkey.WebApi.Middlewares
{
    public class ResponseTimerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseTimerMiddleware> _logger;

        public ResponseTimerMiddleware(RequestDelegate next, ILogger<ResponseTimerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch timer =  Stopwatch.StartNew();
            _logger.LogInformation(context.Request.Path);
            await _next(context);
            timer.Stop();
            _logger.LogInformation(timer.Elapsed.ToString());
            _logger.LogInformation(timer.ElapsedMilliseconds.ToString()+" ms");
            _logger.LogInformation(timer.ElapsedTicks.ToString()+" tick");
        }
    }
}
