using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Custom.TemplateEngine
{
    public class SqlConnectionFactory 
    {
        private readonly IConfiguration _config;

        public SqlConnectionFactory(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection GetNamedConnection(string name)
        {
            return new SqlConnection(_config.GetConnectionString(name));
        }
    }
}
