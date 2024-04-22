using AppTarea.Infraestructura.DTO.DTOs;

namespace AppTarea.Infraestructura.DTO.Mappers
{
    public static class TareaUsuarioMapper
    {
       
        public static TareaUsuario Map(this TareaUsuaioDTO DTO)
        {
            return new TareaUsuario()
            {
                tareaId = DTO.tareaId,
                usuarioId = DTO.usuarioId,
            };
        }

    }
}
