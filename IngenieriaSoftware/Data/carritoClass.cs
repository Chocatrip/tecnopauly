using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace IngenieriaSoftware.Data
{
    public class carritoClass
    {
        [Key]
        public int id_carrito { get; set; }
        public int id_cuenta { get; set; }
        public int activo { get; set; }
    }
}
