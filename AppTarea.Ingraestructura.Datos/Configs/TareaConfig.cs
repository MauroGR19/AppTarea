using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppTarea.Infraestructura.Datos.Configs
{
    public class TareaConfig : IEntityTypeConfiguration<Tarea>
    {
        public void Configure(EntityTypeBuilder<Tarea> builder)
        {
            builder.ToTable("Tarea");
            builder.HasKey(c => c.tareaId);

            builder.HasMany(t => t.Usuarios)
                   .WithMany(u => u.Tareas)
                   .UsingEntity<TareaUsuario>(
                       j => j
                           .HasOne(tu => tu.Usuario)
                           .WithMany()
                           .HasForeignKey(tu => tu.usuarioId),
                       j => j
                           .HasOne(tu => tu.Tarea)
                           .WithMany()
                           .HasForeignKey(tu => tu.tareaId)
                           );
        }
    }
}
