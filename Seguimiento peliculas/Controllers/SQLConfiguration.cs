namespace Seguimiento_peliculas.Controllers
{
    public class SQLConfiguration
    {
        public SQLConfiguration(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }
    }
}
