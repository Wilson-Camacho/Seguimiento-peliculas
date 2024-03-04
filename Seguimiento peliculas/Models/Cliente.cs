using System.ComponentModel.DataAnnotations.Schema;

namespace Seguimiento_peliculas.Models
{
    public class Cliente
    {
        [Column("IdCliente")]
        public int? IdCliente { get; set; }

        [Column("Nombre")]
        public string? Nombre { get; set; }
        
        [Column("Password")]
        public string? Password { get; set; }

    }
}
