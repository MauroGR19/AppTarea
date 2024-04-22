using AppTarea.Infraestructura.Datos.Contexto;

namespace AppVenta.Infraestructura.Datos
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Creando la DB si no existe...");
			AplicattionContexto db = new AplicattionContexto();
			db.Database.EnsureCreated();
			Console.WriteLine("Listo!!!!!");
			Console.ReadKey();
		}
	}
}
