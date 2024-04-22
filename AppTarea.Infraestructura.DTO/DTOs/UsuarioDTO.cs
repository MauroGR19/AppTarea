using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTarea.Infraestructura.DTO.DTOs
{
    public class UsuarioDTO : AuditoriaDTO
    {
        public Guid usuarioId { get; set; }
        public string nombre { get; set; }
        public string Contrasena { get; set; }
        public List<Tarea>? Tareas { get; set; }
    }
}
