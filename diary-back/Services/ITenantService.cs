namespace diary_back.Services
{
    public interface ITenantService
    {
        string GetConnectionString(string tenantId);
    }

}
