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
        public DbSet<carritoClass> carrito { get; set; }
        public DbSet <carrito_detalleClass> carrito_detalle { get; set; }
        public DbSet<ventasClass> ventas { get; set; }
        public DbSet<id_ventasClass> id_ventas { get; set; }
    }
}
