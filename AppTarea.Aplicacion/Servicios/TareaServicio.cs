using AppTarea.Dominio.Interfaces.Repositorio;
using AppTarea.Aplicacion.Interfaces;
using AppTarea.Dominio.Response;
using AppTarea.Dominio.Interfaces;

namespace AppTarea.Aplicacion.Servicios
{
    public class TareaServicio : IServicioBase<Tarea, Guid>
    {
        private readonly IRepositorioBase<Tarea, Guid> repoTarea;
        public TareaServicio(IRepositorioBase<Tarea, Guid> _repoTare)
        {
            repoTarea = _repoTare;
        }

        Task<Response<Tarea>> IAgregar<Tarea>.Agregar(Tarea entidad)
        {
            return repoTarea.Agregar(entidad);
        }

        Task<Response<bool>> IEditar<Tarea>.Editar(Tarea entidad)
        {
            return repoTarea.Editar(entidad);
        }

        Task<Response<bool>> IEliminar<Guid>.Eliminar(Guid entidadId)
        {
            return repoTarea.Eliminar(entidadId);
        }

        Task<Response<List<Tarea>>> IListar<Tarea, Guid>.Listar()
        {
            return repoTarea.Listar();
        }

        Task<Response<Tarea>> IListar<Tarea, Guid>.SeleccionarPorID(Guid entidadID)
        {
            return repoTarea.SeleccionarPorID(entidadID);
        }

        Task<Response<bool>> IAsignarTarea<Tarea>.Asignar(TareaUsuario tUsuario)
        {
            return repoTarea.Asignar(tUsuario);
        }

        Task<Response<bool>> IMarcar<Tarea>.Marcar(Tarea tarea)
        {
            return repoTarea.Marcar(tarea);
        }

    }
}
