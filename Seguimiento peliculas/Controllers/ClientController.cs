using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using Seguimiento_peliculas.Interface;
using Seguimiento_peliculas.Models;
using System.Configuration;

namespace Seguimiento_peliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClient _client;

        public ClientController(IClient cliente) {
            _client = cliente;
        }
        
        [HttpGet]
        public ActionResult Index() {
            return Ok(_client.GetAllClients().Result);
        }

        [HttpPost("login")]
        public IActionResult GetClient([FromBody] Cliente cliente)
        {
            var resultado = _client.GetClient(cliente.Nombre, cliente.Password);

            return Ok(resultado.Result);
        }

        [HttpPost("create")]
        public IActionResult AddClient([FromBody] Cliente cliente)
        {
            var resultado = _client.CreateClient(cliente);

            return Ok(resultado.Result);
        }
    }
}
