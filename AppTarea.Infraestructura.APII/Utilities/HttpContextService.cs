namespace AppTarea.Infraestructura.APII.Utilities
{
    public class HttpContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public HttpContext CurrentHttpContext => _httpContextAccessor.HttpContext!;
    }
}