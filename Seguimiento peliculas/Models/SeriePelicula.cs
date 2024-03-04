using Org.BouncyCastle.Asn1.X509;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seguimiento_peliculas.Models
{
    public class SeriePelicula
    {
        [Column("idSeriesPeliculas")]
        public int idSeriesPeliculas { get; set; }

        [Column("Titulo")]
        public string Titulo { get; set; }
        
        [Column("Genero")]
        public string Genero { get; set; }
    }
}
