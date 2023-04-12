using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Streamish.Repositories
{
    // abstract - indicates that our BaseRepository class cannot be directly instantiated,
    // but can ONLY be used by inheritance.
    public abstract class BaseRepository
    {
        private readonly string _connectionString;

        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //We mark the Connection property as protected to make it available to child classes,
        //but inaccessible to any other code.
        protected SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }
    }
}