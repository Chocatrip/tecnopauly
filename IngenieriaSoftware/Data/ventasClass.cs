using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace IngenieriaSoftware.Data
{
    public class ventasClass
    {
        [Key]
        public int id { get; set; }
        public int id_venta { get; set; }
        public int id_cliente { get; set; }
        public int id_producto { get; set; }
        public string nombre_prod { get; set; }
        public string cod_prod { get; set; }
        public int q_prod { get; set; }
        public int precio_inversion { get; set; }
        public int precio_venta { get; set; }
    }
}
