using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngenieriaSoftware.Models
{
    public class TablaModel
    {
        public DatoTablaModel[] Datos { get; set; }
    }
    public class DatoTablaModel {
        public int id { get; set; }
        public string NombreProducto { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public int PrecioCosto { get; set; }
        public int PrecioVenta { get; set; }
    }
}
