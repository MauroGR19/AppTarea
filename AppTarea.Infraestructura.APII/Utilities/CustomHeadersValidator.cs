using AppTarea.Dominio.Interfaces;
using AppTarea.Dominio.Response;
using System.Text.RegularExpressions;

namespace AppTarea.Infraestructura.APII.Utilities
{
    public class CustomHeadersValidator : ICustomHeadersValidator
    {
        private readonly IConfiguration _configuration;
        private readonly HttpContextService _httpContextService;

        public CustomHeadersValidator(IConfiguration configuration, HttpContextService httpContextService)
        {
            _configuration = configuration;
            _httpContextService = httpContextService;
        }

        public Response<bool> IsValid(string? apiKey, string? userRq, string? dateIn, string? dateXp, string? Issuer , string? IsAudience)
        {
            var contextHttp = _httpContextService.CurrentHttpContext.Request.Path.ToString();
            if (!contextHttp.Contains("api/Usuario"))
            {
                if (string.IsNullOrEmpty(apiKey))
                    return Response<bool>.Success(false, "API Key vacío o no válido.");
                if (_configuration.GetValue<string>("x-api-key") != apiKey)
                    return Response<bool>.Success(false, "API Key no corresponde al configurado.");
                if (string.IsNullOrEmpty(userRq))
                    return Response<bool>.Success(false, "userRq vacío o no válido.");
                if (string.IsNullOrEmpty(dateIn))
                    return Response<bool>.Success(false, "dateIn vacío o no válido.");
                if (string.IsNullOrEmpty(dateXp))
                    return Response<bool>.Success(false, "dateXp vacío o no válido.");
                if (string.IsNullOrEmpty(Issuer) || Issuer != _configuration["Jwt:Issuer"])
                    return Response<bool>.Success(false, "Issuer vacío o no válido.");
                if (string.IsNullOrEmpty(IsAudience) || Issuer != _configuration["Jwt:IsAudience"])
                    return Response<bool>.Success(false, "IsAudience vacío o no válido.");

                //DateTime timeIni = DateTimeOffset.FromUnixTimeSeconds(long.Parse(dateIn)).DateTime;
                //DateTime timeExp = DateTimeOffset.FromUnixTimeSeconds(long.Parse(dateXp)).DateTime;
                //if (DateTime.Now <= timeIni || DateTime.Now >= timeExp)
                //{
                //    return Response<bool>.Success(false, "Fecha token no válido.");
                //}
            }

            return Response<bool>.Success(true, "Ok.");
        }
    }
}