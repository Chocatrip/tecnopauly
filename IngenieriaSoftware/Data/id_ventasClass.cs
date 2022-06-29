using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace IngenieriaSoftware.Data
{
    public class id_ventasClass
    {
        [Key]
        public int id_venta { get; set; }
        public DateTime fecha_ingreso_venta { get; set; }
        public int venta_concretada { get; set; }
        public DateTime fecha_venta { get; set; }
    }
}
