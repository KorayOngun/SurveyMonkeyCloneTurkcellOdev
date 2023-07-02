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

            await _next(context);

            timer.Stop();
            _logger.LogInformation(timer.Elapsed.ToString());
            _logger.LogInformation(timer.ElapsedMilliseconds.ToString());
            _logger.LogInformation(timer.ElapsedTicks.ToString());
        }
    }
}
