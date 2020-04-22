using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;

namespace AspNet31.SimpleInjector.HostedService.Middlewares
{
    public class LivenessMiddleware
    {
        private const string LivenessPathValue = "/ping";

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Path == LivenessPathValue)
            {
                return context.Response.WriteAsync("pong", Encoding.UTF8);
            }

            return next.Invoke(context);
        }
    }
}
