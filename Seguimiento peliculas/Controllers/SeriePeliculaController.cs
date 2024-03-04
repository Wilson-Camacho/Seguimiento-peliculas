using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Seguimiento_peliculas.Interface;
using Seguimiento_peliculas.Models;
using System.Configuration;

namespace Seguimiento_peliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriePeliculaController : ControllerBase
    {
        private readonly ISeriePelicula _seriePelicula;
        public SeriePeliculaController(ISeriePelicula seriePelicula) {
            _seriePelicula = seriePelicula;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return Ok(_seriePelicula.GetAllSerieMovie().Result);
        }

        [HttpGet("getlistseguimiento")]
        public async Task<ActionResult> getListaSeguimiento(int idCliente)
        {
            return Ok(_seriePelicula.GetAllListSeguimiento(idCliente).Result);
        }

        [HttpPost("createseguimiento")]
        public async Task<ActionResult> AddListaSeguimiento([FromBody] ListSeguimiento listSeguimiento)
        {
            listSeguimiento = _seriePelicula.CreateListSeguimiento(listSeguimiento).Result;

            if (listSeguimiento == null) return Conflict("Ya existe la lista");

            return Ok(listSeguimiento); 
        }

        [HttpPost("CreatListSeguimientoPeliculaSerie")]
        public async Task<ActionResult> CreatListSeguimientoPeliculaSerie([FromBody] ListSeguimiento listSeguimiento)
        {
            var resultado = _seriePelicula.CreatListSeguimientoPeliculaSerie(listSeguimiento);

            return Ok(resultado.Result);
        }
    }
}
