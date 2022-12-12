namespace Lesson18
{
    public class CountMiddleware
    {
        private RequestDelegate _next;
        private long amount = 0;
        const string KeyName = "RequestAmount";
        public CountMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.Headers[KeyName] = amount.ToString();
            await _next.Invoke(context);
            if (context.Response.StatusCode / 100 == 2) ++amount;
        }
    }
}
