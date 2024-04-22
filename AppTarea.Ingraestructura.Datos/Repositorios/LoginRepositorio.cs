using AppTarea.Dominio.Interfaces.Repositorio;
using AppTarea.Infraestructura.Datos.Contexto;
using AppTarea.Dominio.Modelos;
using AppTarea.Dominio.Response;
using AppTarea.Dominio.Interfaces;

namespace AppTarea.Infraestructura.Datos.Repositorios
{
    public class LoginRepositorio : IRrepositorioLogin<Login>
    {
        private AplicattionContexto db;
        public LoginRepositorio(AplicattionContexto _db)
        {
            db = _db;
        }
        
        public Task<Response<Usuario>> loguear(Login entidad)
        {
            try
            {
                var result = db.usuario.Where(a => a.nombre == entidad.userName).FirstOrDefault();
                if (result == null)
                    return Task.FromResult(Response<Usuario>.Success(null!, "")); 

                if(result.nombre != entidad.userName || result.Contrasena != entidad.password)
                    return Task.FromResult(Response<Usuario>.Success(null!, "Datos incorrectos o invalidos."));

                return Task.FromResult(Response<Usuario>.Success(result, "Operación existosa."));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Response<Usuario>.Error(null!, "Error Interno del Servidor.", ex));
            }
        }

        Task<Response<Usuario>> IAgregar<Usuario>.Agregar(Usuario entidad)
        {

            try
            {
                entidad.usuarioId = Guid.NewGuid();

                db.usuario.Add(entidad);
                db.SaveChanges();
                return Task.FromResult(Response<Usuario>.Success(entidad, "Usuario agregado exitosamente!!!"));

            }
            catch (Exception ex)
            {
                return Task.FromResult(Response<Usuario>.Error(null, "Ocurrió un error al agregar el usuario.", ex));
            }

        }
    }
}
