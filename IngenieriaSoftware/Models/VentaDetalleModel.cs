using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace IngenieriaSoftware.Models
{
    public class VentaDetalleModel
    {
        public int id_venta { get; set; }
        public int id_cliente { get; set; }
        public DateTime fecha_ingreso_venta { get; set; }
        public VentaDetalleItem[] itemsVenta { get; set; }
        public int total_precio { get; set; }
        public int total_ganancia { get; set; }
        
    }

    public class VentaDetalleItem {
        public int id_producto { get; set; }
        public string nombre_prod { get; set; }
        public string cod_prod { get; set; }
        public int stockDisponible { get; set; }
        public int q_prod { get; set; }
        public int precio_inversion { get; set; }
        public int precio_venta { get; set; }
        public int checkVenta { get; set; }
    }
}
