using System.ComponentModel.DataAnnotations.Schema;

namespace AppTarea.Infraestructura.DTO.DTOs
{
    public class TareaDTO : AuditoriaDTO
    {
        public Guid tareaId { get; set; }
        public DateTime fechaAdicion { get; set; }
        public string descripcion { get; set; }
        public string estado { get; set; }
        public string prioridad { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public string comentarios { get; set; }
        
        public List<UsuarioDTO>? Usuarios { get; set; }
    }
    
}
