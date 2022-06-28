using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace IngenieriaSoftware.Data
{
    public class carrito_detalleClass
    {
        [Key]
        public int id_carrito_detalle { get; set; }
        public int id_carrito { get; set; }
        public int id_producto { get; set; }
        public int q_producto { get; set; }
        public string nombre_producto { get; set; }
        public int precio_inversion { get; set; }
        public int precio_venta { get; set; }
        public int vendido { get; set; }
        public DateTime fecha_venta { get; set; }

    }
}
