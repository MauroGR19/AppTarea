using AppTarea.Dominio.Interfaces;
using AppTarea.Dominio.Interfaces.Repositorio;
using AppTarea.Dominio.Response;
using AppTarea.Infraestructura.Datos.Contexto;

namespace AppTarea.Infraestructura.Datos.Repositorios
{
    public class TareaRepositorio : IRepositorioBase<Tarea, Guid>
    {
        private AplicattionContexto db;
        public TareaRepositorio(AplicattionContexto _db)
        {
            db = _db;
        }

        Task<Response<Tarea>> IAgregar<Tarea>.Agregar(Tarea entidad)
        {
            try
            {
                entidad.tareaId = Guid.NewGuid();

                if (entidad.comentarios.Length > 200)
                    return Task.FromResult(Response<Tarea>.Success(null!, "El campo comentario no puede ser mayor a 200 caracteres."));

                if (entidad.prioridad.ToUpper() == "ALTA" && entidad.comentarios == "")
                    return Task.FromResult(Response<Tarea>.Success(null!, "El comentario es obligatorio."));

                if (entidad.prioridad.ToUpper() != "ALTA" && entidad.prioridad.ToUpper() != "MEDIA" && entidad.prioridad.ToUpper() != "BAJA")
                    return Task.FromResult(Response<Tarea>.Success(null!, "La proridad no es valida o esta vacia."));

                if (entidad.prioridad.ToUpper() == "ALTA")
                {
                    // Calcular la diferencia de días entre la fecha de inicio y la fecha de finalización
                    TimeSpan diferencia = entidad.fechaFin - entidad.fechaInicio;

                    // Verificar si la diferencia es mayor a 15 días
                    if (diferencia.TotalDays > 15)
                        return Task.FromResult(Response<Tarea>.Error(null!, "La Fecha Fin no debe superar por más de 15 días la Fecha Inicio.", null));
                }

                if (entidad.prioridad.ToUpper() == "ALTA")
                {
                    // Calcular la diferencia de días entre la fecha de inicio y la fecha de finalización
                    TimeSpan diferencia = entidad.fechaFin - entidad.fechaInicio;

                    // Verificar si la diferencia es mayor a 2 días
                    if (diferencia.TotalDays > 2)
                        return Task.FromResult(Response<Tarea>.Error(null!, "La Fecha Fin para la prioridad alta no debe ser mayor a 2 días desde la Fecha Inicio.", null));
                }

                if (entidad.estado.ToUpper() != "NUEVA" && entidad.estado.ToUpper() != "ACTIVA" && entidad.estado.ToUpper() != "EN PROCESO" && entidad.estado.ToUpper() != "FINALIZADA" && entidad.estado.ToUpper() != "CANCELADA")
                    return Task.FromResult(Response<Tarea>.Error(null!, "El estado no es válido.", null));

                if (entidad.fechaInicio < DateTime.Today)
                    return Task.FromResult(Response<Tarea>.Error(null!, "La Fecha de Inicio no puede ser menor a la fecha actual.", null));

                db.tareas.Add(entidad);
                db.SaveChanges();
                return Task.FromResult(Response<Tarea>.Success(entidad, "Tarea agregada exitosamente!!!"));

            }
            catch (Exception ex)
            {
                return Task.FromResult(Response<Tarea>.Error(null, "Ocurrió un error al agregar la tarea.", ex));
            }
        }

        Task<Response<bool>> IEditar<Tarea>.Editar(Tarea entidad)
        {
            try
            {
                var tareaSeleccionada = db.tareas.FirstOrDefault(c => c.tareaId == entidad.tareaId);
                if (tareaSeleccionada != null)
                {

                    if (tareaSeleccionada.estado.ToUpper() == "FINALIZADA")
                        return Task.FromResult(Response<bool>.Error(false, "No se puede editar una tarea que cuenta con el estado Finalizada.", null));

                    if (tareaSeleccionada.prioridad.ToUpper() == "ALTA" && entidad.estado.ToUpper() == "EN PROCESO")
                        return Task.FromResult(Response<bool>.Error(false, "No se puede editar una tarea que cuenta con la prioridad Alta y estado En Proceso.", null));

                    if (entidad.fechaFin < entidad.fechaInicio)
                        return Task.FromResult(Response<bool>.Error(false, "La Fecha Fin no puede ser menor a la Fecha Inicio.", null));

                    if (entidad.fechaFin < DateTime.Today && entidad.estado.ToUpper() != "CANCELADA")
                        return Task.FromResult(Response<bool>.Error(false, "Si la Fecha Fin es menor a la fecha actual, solo se puede cambiar el estado a Cancelada.", null));

                    // Aplicar las ediciones restantes
                    tareaSeleccionada.estado = entidad.estado;
                    tareaSeleccionada.fechaFin = entidad.fechaFin;
                    tareaSeleccionada.comentarios = entidad.comentarios;

                    db.Entry(tareaSeleccionada).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    return Task.FromResult(Response<bool>.Success(true, "La tarea se editó correctamente."));
                }

                return Task.FromResult(Response<bool>.Error(false, "La tara no fue encontrada.", null));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Response<bool>.Error(false, "Ocurrió un error al editar la tarea.", ex));
            }
        }

        Task<Response<bool>> IEliminar<Guid>.Eliminar(Guid entidadId)
        {
            try
            {
                var tareasSeleccionada = db.tareas.Where(c => c.tareaId == entidadId).FirstOrDefault();
                if (tareasSeleccionada != null)
                {
                    if (tareasSeleccionada.estado.ToUpper() == "FINALIZADA" || tareasSeleccionada.estado.ToUpper() == "EN PROCESO")
                        return Task.FromResult(Response<bool>.Error(false, "No se pueden eliminar tareas con los estados Finalizada, En Proceso.", null));

                    if (DateTime.Today > tareasSeleccionada.fechaFin)
                        return Task.FromResult(Response<bool>.Error(false, "No se pueden eliminar tareas que ya cumplan el tiempo límite de ejecución (Fecha Fin).", null));

                    if (tareasSeleccionada.prioridad.ToUpper() == "ALTA" && tareasSeleccionada.estado.ToUpper() != "NUEVA")
                        return Task.FromResult(Response<bool>.Error(false, "Una tarea con prioridad alta solo puede eliminarse cuando el estado es Nueva.", null));

                    db.tareas.Remove(tareasSeleccionada);
                    db.SaveChanges();
                    return Task.FromResult(Response<bool>.Success(true, "La tarea se elimino correctamente."));
                }


                return Task.FromResult(Response<bool>.Error(false, "La tara no fue encontrada.", null));


            }
            catch (Exception ex)
            {
                return Task.FromResult(Response<bool>.Error(false, "Ocurrió un error al Eliminar la tarea.", ex));
            }
        }

        Task<Response<List<Tarea>>> IListar<Tarea, Guid>.Listar()
        {
            try
            {
                var tareas = db.tareas.ToList();
                foreach (var item in tareas)
                {
                    item.Usuarios = (from tarea in db.tareas
                                     join tareaUsuario in db.tareaUsuario on tarea.tareaId equals tareaUsuario.tareaId
                                     join Usuario in db.usuario on tareaUsuario.usuarioId equals Usuario.usuarioId
                                     where tarea.tareaId == item.tareaId
                                     select Usuario).ToList();

                }
                return Task.FromResult(Response<List<Tarea>>.Success(tareas, "Operacion exitosa"));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Response<List<Tarea>>.Error(null, "Ocurrió un error al listar las tareas.", ex));
            }
        }

        Task<Response<Tarea>> IListar<Tarea, Guid>.SeleccionarPorID(Guid entidadID)
        {
            var tareasSeleccionada = db.tareas.Where(c => c.tareaId == entidadID).FirstOrDefault();
            return Task.FromResult(Response<Tarea>.Success(tareasSeleccionada, ""));
        }

        public Task<Response<bool>> Asignar(TareaUsuario tUsuario)
        {
            try
            {
                var usuario = db.usuario.FirstOrDefault(c => c.usuarioId == tUsuario.usuarioId);
                var tarea = db.tareas.FirstOrDefault(c => c.tareaId == tUsuario.tareaId);

                if (usuario == null || tarea == null)
                {
                    return Task.FromResult(Response<bool>.Error(false, "La tarea o usuario invalidos.", null));
                }

                db.tareaUsuario.Add(tUsuario);
                db.SaveChanges();
                return Task.FromResult(Response<bool>.Success(true, "La tarea fue asignada correctamente."));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Response<bool>.Error(false, "Ocurrió un error al Asignar la tarea.", ex));
            }
        }

        public Task<Response<bool>> Marcar(Tarea tarea)
        {
            try
            {
                var usuario = db.tareas.FirstOrDefault(c => c.tareaId == tarea.tareaId);
                
                if (tarea == null)
                {
                    return Task.FromResult(Response<bool>.Error(false, "La tarea no fue encontrada.", null));
                }

                db.tareas.Add(tarea);
                db.SaveChanges();
                return Task.FromResult(Response<bool>.Success(true, "El estado de la tarea a sigo actualizado corecctamente."));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Response<bool>.Error(false, "Ocurrió un error al Asignar la tarea.", ex));
            }
        }
    }
}
