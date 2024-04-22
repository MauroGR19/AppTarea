using Microsoft.EntityFrameworkCore;
using AppTarea.Dominio;
using AppTarea.Infraestructura.Datos.Configs;

namespace AppTarea.Infraestructura.Datos.Contexto
{
    public class AplicattionContexto : DbContext
	{
		public DbSet<Tarea> tareas { get; set; }
		public DbSet<TareaUsuario> tareaUsuario { get; set; }
		public DbSet<Usuario> usuario { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			options.UseSqlServer("Data Source=LAPTOP-B0U91KVL\\SQLEXPRESS;Initial Catalog=Tareas;Integrated Security=false;User ID=userTarea;Password=tarea2024.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfiguration(new TareaConfig());	
			builder.ApplyConfiguration(new TareaUsuarioConfig());	
			builder.ApplyConfiguration(new UsuarioConfig());	

		}
	}
}
