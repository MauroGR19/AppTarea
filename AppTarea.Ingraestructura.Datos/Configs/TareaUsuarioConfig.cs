using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppTarea.Infraestructura.Datos.Configs
{
    public class TareaUsuarioConfig : IEntityTypeConfiguration<TareaUsuario>
    {
        public void Configure(EntityTypeBuilder<TareaUsuario> builder)
        {
            builder.ToTable("TareaUsuario");

            builder.HasKey(tu => new { tu.tareaId, tu.usuarioId });
        }
    }
}
