using Luval.Framework.Security.Authorization;
using Luval.Framework.Security.Authorization.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Diagnostics;

namespace Luval.Framework.Security.MySql
{
    public class MySqlAuthorizationDbContext : AuthorizationDbContext, IAuthorizationDbContext
    {

        private string _connectionString;
        public MySqlAuthorizationDbContext(string connStr) : base()
        {
            _connectionString = connStr;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.
                UseMySQL(_connectionString).UseLazyLoadingProxies(true);
        }
    }
}