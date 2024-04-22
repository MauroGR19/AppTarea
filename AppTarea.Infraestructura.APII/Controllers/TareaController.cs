using Microsoft.AspNetCore.Mvc;
using AppTarea.Aplicacion.Servicios;
using AppTarea.Infraestructura.Datos.Contexto;
using AppTarea.Infraestructura.Datos.Repositorios;
using AppTarea.Infraestructura.APII.Utilities;
using AppTarea.Dominio.Response;
using System.Net;
using AppTarea.Dominio.Interfaces;
using AppTarea.Dominio.Interfaces.Repositorio;
using AppTarea.Infraestructura.DTO.DTOs;
using AppTarea.Infraestructura.DTO.Mappers;
using AppTarea.Aplicacion.Interfaces;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppTarea.Infraestructura.APII.Controllers
{
    [CustomHeaders]
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly IServicioBase<Tarea, Guid> db;

        public TareaController(IServicioBase<Tarea, Guid> _db)
        {
            db = _db;
        }
       
        // GET: api/<TareaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
             var respuesta = await db.Listar();
             return Ok(new APIResponseList<TareaDTO>()
            {
                Code = (int)HttpStatusCode.OK,
                Status = (int)HttpStatusCode.OK,
                Mensaje = respuesta.Mensaje,
                Data = respuesta.Data.Map()

            }) ;
        }

        // GET api/<TareaController>/5
        [HttpGet("{id}")]
        public ActionResult<Tarea> Get(Guid id)
        {
            return Ok(db.SeleccionarPorID(id));
        }

        // POST api/<TareaController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TareaDTO tarea)
        {

            var respuesta = await db.Agregar(tarea.Map());
            if (!respuesta.IsSuccess || respuesta.Data == null )
            {
                return Response<bool>.ContentError(code: (int)HttpStatusCode.BadRequest, status: (int)HttpStatusCode.BadRequest, respuesta.Mensaje);
            }

            return Ok(new APIResponse()
            {
                Code = (int)HttpStatusCode.OK,
                Status = (int)HttpStatusCode.OK,
                Mensaje = respuesta.Mensaje
            }) ;
        }

        // PUT api/<TareaController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TareaDTO tarea)
        {
            
            var respuesta = await db.Editar(tarea.Map());
            if (!respuesta.IsSuccess || !respuesta.Data)
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

        // DELETE api/<TareaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            
            var respuesta= await db.Eliminar(id);
            if (!respuesta.IsSuccess || !respuesta.Data)
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

        //Asignar
        [HttpPost("Asignar")]
        public async Task<IActionResult> Asignar ([FromBody] TareaUsuaioDTO tUsuario)
        {
            var respuesta = await db.Asignar(tUsuario.Map());
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

        [HttpPost("Marcar")]
        public async Task<IActionResult> Marcar([FromBody] TareaDTO tarea)
        {
            var respuesta = await db.Marcar(tarea.Map());
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
