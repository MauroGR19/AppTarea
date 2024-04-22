using AppTarea.Dominio.Modelos;
using AppTarea.Infraestructura.DTO.DTOs;

namespace AppTarea.Infraestructura.DTO.Mappers
{
    public static class LoginMapper
    {
        
        public static Login Map(this LoginDTO DTO)
        {
            return new Login()
            {
                userName = DTO.userName,
                password = DTO.password,    

            };
        }
    }
}
