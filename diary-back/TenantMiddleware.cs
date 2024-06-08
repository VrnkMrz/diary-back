namespace diary_back
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var tenantId = context.Request.Headers["X-Tenant-ID"].FirstOrDefault();

            if (!string.IsNullOrEmpty(tenantId))
            {
                context.Items["TenantId"] = tenantId;
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Tenant ID is missing.");
                return;
            }


            await _next(context);
        }
    }

}
