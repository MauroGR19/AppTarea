using AppTarea.Infraestructura.DTO.DTOs;

namespace AppTarea.Infraestructura.DTO.Mappers
{
    public static class UsuarioMapper
    {
        public static UsuarioDTO Map(this Usuario model)
        {
            return new UsuarioDTO()
            {
              usuarioId = model.usuarioId,
              nombre = model.nombre,
              Contrasena = model.Contrasena,
            };
        }

        public static List<UsuarioDTO> Map(this List<Usuario> model)
        {
            List<UsuarioDTO> Dtos = new List<UsuarioDTO>();

            foreach (Usuario modelItem in model)
            {
                Dtos.Add(Map(modelItem));
            }

            return Dtos;
        }

        public static Usuario Map(this UsuarioDTO DTO)
        {
            return new  Usuario()
            {
                usuarioId = DTO.usuarioId,
                nombre = DTO.nombre,
                Contrasena = DTO.Contrasena,
                Creado = DTO.Creado,    
                CreadoPor = DTO.CreadoPor,
                Actualizado = DTO.Actualizado,
                ActualizadoPor = DTO.ActualizadoPor
            };
        }
    }
}
