using Microsoft.AspNetCore.Mvc;
using AppTarea.Aplicacion.Servicios;
using AppTarea.Dominio.Modelos;
using AppTarea.Infraestructura.Datos.Contexto;
using AppTarea.Infraestructura.Datos.Repositorios;
using AppTarea.Dominio.Response;
using System.Net;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using AppTarea.Infraestructura.DTO.DTOs;
using AppTarea.Infraestructura.DTO.Mappers;
using AppTarea.Aplicacion.Interfaces;

namespace AppTarea.Infraestructura.APII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IConfiguration configuration;

        private readonly IServicioLogin<Login> db;
        
        public UsuarioController(IServicioLogin<Login> _db, IConfiguration _configuration)
        {
            db = _db;
            configuration = _configuration;
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Login login)
        {
            
            var respuest = await db.loguear(login);
            if (!respuest.IsSuccess)
            {
                return Response<bool>.ContentError(code: (int)HttpStatusCode.BadRequest, status: (int)HttpStatusCode.BadRequest, "");
            }
            Usuario user = respuest.Data;
            var claims = new[]
            {
                new Claim("name", user.nombre),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            byte[] keyBytes = Convert.FromBase64String(configuration["Jwt:key"]);
            var key = new SymmetricSecurityKey(keyBytes);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:IsAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                notBefore: DateTime.Now.AddMinutes(-10),
                signingCredentials: creds
            );

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Ok(new APIResponseLogin()
            {
                Token = token,
                Expiration = DateTime.Now.AddMinutes(120),
                Code = (int)HttpStatusCode.OK,
                Status = (int)HttpStatusCode.OK,    
                User = respuest.Data,
                Mensaje = respuest.Mensaje 
            }) ;
        }

        [HttpPost("Agregar")]
        public async Task<IActionResult> Agregar([FromBody] UsuarioDTO usuario)
        {
            var respuesta = await db.Agregar(usuario.Map());
            if (!respuesta.IsSuccess || respuesta.Data == null)
            {
                return Response<bool>.ContentError(code: (int)HttpStatusCode.BadRequest, status: (int)HttpStatusCode.BadRequest, respuesta.Mensaje);
            }

            return Ok(new APIResponse()
            {
                Code = (int)HttpStatusCode.OK,
                Status = (int)HttpStatusCode.OK,
                Mensaje = respuesta.Mensaje
            });
        }
    }
}
