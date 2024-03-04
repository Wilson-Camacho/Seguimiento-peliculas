using Seguimiento_peliculas.Models;

namespace Seguimiento_peliculas.Interface
{
    public interface IClient
    {
        Task<List<Cliente>> GetAllClients();
        Task<Cliente> GetClient(string username, string password);

        Task<Cliente> CreateClient(Cliente client);
    }
}
