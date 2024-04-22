using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTarea.Dominio.Interfaces;
using AppTarea.Dominio.Interfaces.Repositorio;

namespace AppTarea.Aplicacion.Interfaces
{
    public interface IServicioBase<TEntidad, TEntidadID>
        :IAgregar<TEntidad>, IEditar<TEntidad>, IEliminar<TEntidadID>, IListar<TEntidad, TEntidadID>, IAsignarTarea<TEntidad>, IMarcar<TEntidad>
    {

    }
}
