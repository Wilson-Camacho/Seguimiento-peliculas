using Seguimiento_peliculas.Models;

namespace Seguimiento_peliculas.Interface
{
    public interface ISeriePelicula
    {
        Task<List<SeriePelicula>> GetAllSerieMovie();

        Task<ListSeguimiento> CreateListSeguimiento(ListSeguimiento listaSeguimiento);

        Task<List<ListSeguimiento>> GetAllListSeguimiento(int idCliente);

        Task<ListSeguimiento> CreatListSeguimientoPeliculaSerie(ListSeguimiento listSeguimiento);
    }
}
