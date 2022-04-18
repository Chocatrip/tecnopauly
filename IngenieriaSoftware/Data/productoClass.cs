using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngenieriaSoftware.Data
{
    public class productoClass
    {
        public int id { get; set; }
        public string nombre_producto { get; set; }
        public string categoria { get; set; }
        public string descripcion { get; set; }
        public string codigo { get; set; }
        public int precio_costo { get; set; }
        public int precio_venta { get; set; }
    }
}
