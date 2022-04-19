using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IngenieriaSoftware.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<pruebaClass> prueba { get; set; }
        public DbSet<productoClass> producto { get; set; }
        public DbSet<usuarioClass> usuario { get; set; }
        public DbSet<cuentaClass> cuenta { get; set; }
    }
}
