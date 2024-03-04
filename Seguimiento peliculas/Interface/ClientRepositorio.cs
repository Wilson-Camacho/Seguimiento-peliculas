using Dapper;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Seguimiento_peliculas.Controllers;
using Seguimiento_peliculas.Models;

namespace Seguimiento_peliculas.Interface
{
    public class ClientRepositorio : IClient
    {
        private readonly SQLConfiguration _configuration;
        public ClientRepositorio(SQLConfiguration configuration) {
            _configuration = configuration;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_configuration.ConnectionString);
        }

        public async Task<List<Cliente>> GetAllClients()
        {
            var db = dbConnection();
            var queryExpression = @"SELECT * FROM clientes";

            return db.QueryAsync<Cliente>(queryExpression, new { }).Result.ToList();
        }

        public async Task<Cliente> GetClient(string username, string password)
        {
            var db = dbConnection();

            await db.OpenAsync();

            var queryExpression = @$"SELECT * FROM clientes WHERE Nombre = @username AND Password = @password";
            var parameters = new { username = username, password = password };

            return await db.QueryFirstOrDefaultAsync<Cliente>(queryExpression, parameters);
        }

        public async Task<Cliente> CreateClient(Cliente client)
        {
            var db = dbConnection();

            await db.OpenAsync();
            var parameters = new { username = client.Nombre, password = client.Password };

            var queryExpression = @$"SELECT * FROM clientes WHERE Nombre = @username AND Password = @password";

            var result = await db.QueryFirstOrDefaultAsync<Cliente>(queryExpression, parameters);

            if (result != null) {
                return null;
            }

            queryExpression = @$"INSERT INTO clientes (Nombre, Password) VALUES (@username, @password)";
            return await db.ExecuteScalarAsync<Cliente>(queryExpression, parameters);
        }
    }
}
