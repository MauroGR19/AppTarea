using AppTarea.Infraestructura.DTO.DTOs;


namespace AppTarea.Infraestructura.DTO.Mappers
{
    public static class TareaMapper
    {
        public static TareaDTO Map(this Tarea model)
        {
            return new TareaDTO()
            {
                Creado = model.Creado,
                CreadoPor = model.CreadoPor,
                Actualizado = model.Actualizado,
                ActualizadoPor = model.ActualizadoPor,
                tareaId = model.tareaId,
                fechaAdicion = model.fechaAdicion,
                descripcion = model.descripcion,
                estado = model.estado,
                prioridad = model.prioridad,
                fechaInicio = model.fechaInicio,
                fechaFin = model.fechaFin,
                comentarios = model.comentarios,
                Usuarios = model.Usuarios.Map()
            };
        }

        public static List<TareaDTO> Map(this List<Tarea> model)
        {
            List<TareaDTO> Dtos = new List<TareaDTO>();


            foreach (Tarea modelItem in model)
            {
                Dtos.Add(Map(modelItem));
            }

            return Dtos;
        }

        public static Tarea Map(this TareaDTO DTO)
        {
            return new Tarea()
            {
                Creado = DTO.Creado,
                CreadoPor = DTO.CreadoPor,
                Actualizado = DTO.Actualizado,
                ActualizadoPor = DTO.ActualizadoPor,
                tareaId = DTO.tareaId,
                fechaAdicion = DTO.fechaAdicion,
                descripcion = DTO.descripcion,
                estado = DTO.estado,
                prioridad = DTO.prioridad,
                fechaInicio = DTO.fechaInicio,
                fechaFin = DTO.fechaFin,
                comentarios = DTO.comentarios,


            };
        }
    }
}