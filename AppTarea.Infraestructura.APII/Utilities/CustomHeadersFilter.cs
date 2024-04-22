using AppTarea.Dominio.Interfaces;
using AppTarea.Dominio.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace AppTarea.Infraestructura.APII.Utilities
{
    public class CustomHeadersFilter : IAuthorizationFilter
    {
        private readonly ICustomHeadersValidator _customHeadersValidator;
        private readonly JWToken _jsonWebToken;

        public CustomHeadersFilter(ICustomHeadersValidator customHeadersValidator, JWToken jsonWebToken)
        {
            _customHeadersValidator = customHeadersValidator;
            _jsonWebToken = jsonWebToken;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string? apiKey = context.HttpContext.Request.Headers["x-api-key"];
            var http = context.HttpContext.Request.Path.ToString();
            if (http.Contains("api/User/login"))
            {
                var validation = _customHeadersValidator.IsValid(apiKey, null, null, null, null, null);

                if (validation.IsSuccess && !validation.Data)
                {
                    context.Result = new ContentResult()
                    {
                        Content = JsonConvert.SerializeObject(new APIResponse() { Code = (int)HttpStatusCode.Unauthorized, Status = (int)HttpStatusCode.Unauthorized, Mensaje = "No autorizado." }),
                        ContentType = "application/json",
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }
            }
            else
            {
                var bearer = _jsonWebToken.DecodificarJwtAsync(context.HttpContext.Request.Headers["Authorization"]).Result;
                if (bearer.Data == null)
                {
                    context.Result = new ContentResult()
                    {
                        Content = JsonConvert.SerializeObject(new APIResponse() { Code = (int)HttpStatusCode.Unauthorized, Status = (int)HttpStatusCode.Unauthorized, Mensaje = "No autorizado." }),
                        ContentType = "application/json",
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }
                else
                {
                    string userRq = bearer.Data!.Claims.FirstOrDefault(c => c.Type == "name")!.Value;
                    string Issuer = bearer.Data!.Claims.FirstOrDefault(c => c.Type == "iss")!.Value;
                    string IsAudience = bearer.Data!.Claims.FirstOrDefault(c => c.Type == "aud")!.Value;
                    string dateIn = bearer.Data!.Claims.FirstOrDefault(c => c.Type == "nbf")!.Value;
                    string dateXp = bearer.Data!.Claims.FirstOrDefault(c => c.Type == "exp")!.Value;

                    var validation = _customHeadersValidator.IsValid(apiKey, userRq, dateIn, dateXp, Issuer, IsAudience);

                    if (validation.IsSuccess && !validation.Data)
                    {
                        context.Result = new ContentResult()
                        {
                            Content = JsonConvert.SerializeObject(new APIResponse() { Code = (int)HttpStatusCode.Unauthorized, Status = (int)HttpStatusCode.Unauthorized, Mensaje = "No autorizado." }),
                            ContentType = "application/json",
                            StatusCode = (int)HttpStatusCode.BadRequest
                        };
                    }
                }
            }
        }
    }
}