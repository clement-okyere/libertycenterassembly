using LibertyFamilySystem.Models.Options;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibertyFamilySystem.Repositories
{
    public class PgRepository
    {
        private readonly ConnectionOptions _options;

        public PgRepository(IOptionsSnapshot<ConnectionOptions> options)
        {
            _options = options.Value;
        }

        internal async Task<NpgsqlConnection> GetConnection()
        {
            var connString = "";         
            connString = _options.DefaultConnection;
            var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            return conn;
        }
    }
}
