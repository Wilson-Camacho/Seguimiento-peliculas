using Dapper;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Seguimiento_peliculas.Controllers;
using Seguimiento_peliculas.Models;

namespace Seguimiento_peliculas.Interface
{
    public class SeriePeliculaRepositorio : ISeriePelicula
    {
        private readonly SQLConfiguration _configuration;

        public SeriePeliculaRepositorio(SQLConfiguration configuration) {
            _configuration = configuration;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_configuration.ConnectionString);
        }


        public async Task<List<SeriePelicula>> GetAllSerieMovie()
        {
            var db = dbConnection();
            var queryExpression = @"SELECT * FROM seriespeliculas";

            return db.QueryAsync<SeriePelicula>(queryExpression, new { }).Result.ToList();
        }

        public async Task<ListSeguimiento> CreateListSeguimiento(ListSeguimiento listaSeguimiento) {
            var db = dbConnection();

            var parameters = new { nombreLista = listaSeguimiento.nombreLista, idCliente = listaSeguimiento.idCliente };

            var queryExpression = @$"SELECT * FROM listaseguimiento WHERE nombreLista = @nombreLista AND idCliente = @idCliente";
            
            var result = await db.QueryFirstOrDefaultAsync<Cliente>(queryExpression, parameters);

            if (result != null)
            {
                return null;
            }

            queryExpression = @$"INSERT INTO listaseguimiento (nombreLista, idCliente) VALUES (@nombreLista, @idCliente); SELECT LAST_INSERT_ID();";
            listaSeguimiento.idlistaseguimiento = db.ExecuteScalar<int>(queryExpression, parameters);

            return listaSeguimiento;

        }

        public async Task<List<ListSeguimiento>> GetAllListSeguimiento(int idCliente)
        {
            var db = dbConnection();

            var parameters = new {idCliente = idCliente };
            var queryExpression = @$"SELECT idlistaseguimiento, nombreLista, idCliente FROM listaseguimiento WHERE idCliente = @idCliente";
            List<ListSeguimiento> result = db.QueryAsync<ListSeguimiento>(queryExpression, parameters).Result.ToList();

            foreach (var item in result) {
                queryExpression = @"SELECT idPeliculaSerie FROM listaseguimientopeliculaserie WHERE idListaSeguimiento = @idListaSeguimiento";

                item.SeriePelicula = db.QueryAsync<int>(queryExpression, new { idListaSeguimiento = item.idlistaseguimiento }).Result.ToList();
            }

            return result;


        }

        public async Task<ListSeguimiento> CreatListSeguimientoPeliculaSerie(ListSeguimiento listSeguimiento)
        {
            var db = dbConnection();
            var parameters = new { idSeriePelicula = listSeguimiento.SeriePelicula.First(), idListaSeguimiento = listSeguimiento.idlistaseguimiento };

            var queryExpression = @$"INSERT INTO listaseguimientopeliculaserie (idListaSeguimiento, idPeliculaSerie) VALUES (@idListaSeguimiento, @idSeriePelicula)";
            var result = await db.ExecuteScalarAsync<ListSeguimiento>(queryExpression, parameters);
            return result;
        }
    }
}
