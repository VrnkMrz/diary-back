namespace diary_back.Services
{
    public class TenantService : ITenantService
    {
        private readonly IConfiguration _configuration;

        public TenantService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString(string tenantId)
        {
            return _configuration.GetConnectionString($"Tenant_{tenantId}");
        }
    }
}
