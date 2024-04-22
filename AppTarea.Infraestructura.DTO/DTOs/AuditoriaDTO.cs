using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTarea.Infraestructura.DTO.DTOs
{
    public class AuditoriaDTO
    {
        public DateTime Creado { get; set; }
        public String CreadoPor { get; set; }
        public DateTime Actualizado { get; set; }
        public String ActualizadoPor { get; set; }
    }
}
