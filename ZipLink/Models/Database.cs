using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ZipLink.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class Database : DbContext
    {
        public DbSet<Link> Links { get; set; }

        public Database() : base("database")
        {
        }
    }
}