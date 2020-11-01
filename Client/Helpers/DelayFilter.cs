using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Client.Helpers
{
    public class DelayFilter : IAsyncActionFilter
    {
        private int _delayInMs;
        public DelayFilter(IConfiguration configuration)
        {
            _delayInMs = configuration.GetValue<int>("ApiDelayDuration", 0);
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await Task.Delay(_delayInMs);
            await next();
        }
    }
}
