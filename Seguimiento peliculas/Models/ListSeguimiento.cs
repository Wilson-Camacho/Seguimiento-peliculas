namespace Seguimiento_peliculas.Models
{
    public class ListSeguimiento
    {
        public int? idlistaseguimiento { get; set; }

        public String nombreLista { get; set; }

        public int idCliente { get; set; }

        public List<int>? SeriePelicula {get; set;}
    }
}
