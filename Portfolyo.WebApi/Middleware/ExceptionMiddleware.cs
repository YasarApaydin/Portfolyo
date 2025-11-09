
namespace Portfolyo.WebApi.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try {
                await next(context);
            
            }catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }


        private  Task HandleExceptionAsync(HttpContext httpContext,Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
           httpContext.Response.StatusCode = 400;
            var errorObj = new { message = ex.Message };
            var json = System.Text.Json.JsonSerializer.Serialize(errorObj);

            return httpContext.Response.WriteAsync(json);
        }


    }
}
