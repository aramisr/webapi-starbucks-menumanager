namespace Starbucks.MenuManager.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionHandlingMiddleware
        (
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger,
            IHostEnvironment env
        )
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                //Si ocurre un error en la ejecucion de mi programa
                await _next(context);
            }
            catch(Exception e )
            {

            }
        }
    }
}
