using Microsoft.AspNetCore.Mvc;

namespace AppTarea.Infraestructura.APII.Utilities
{
    public class CustomHeadersAttribute : ServiceFilterAttribute
    {
        public CustomHeadersAttribute() : base(typeof(CustomHeadersFilter))
        {
        }
    }
}