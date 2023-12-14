using ProyectoMusica.Models;
using X.PagedList;

namespace ProyectoMusica.ViewModels
{
    public class ListadoGruposViewModel
    {
        public int? Pagina { get; set; }
        public int Total { get; set; } = 0;
        public IPagedList<Grupo> ListaGrupos { get; set; }
    }
}
