using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Middlewares
{
    public class LivenessMiddleware : IMiddleware
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
