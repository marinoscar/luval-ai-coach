using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Coach.Data.MySql
{
    public class MySqlCoachDbContext : CoachDbContext
    {
        private string _connectionString;

        public MySqlCoachDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString).UseLazyLoadingProxies();
        }
    }
}
