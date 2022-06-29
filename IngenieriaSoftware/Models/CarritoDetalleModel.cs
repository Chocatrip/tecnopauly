namespace IngenieriaSoftware.Models
{
    public class CarritoDetalleModel
    {
        public int IdCarrito { get; set; }
        public int IdUsuario { get; set; }
        public CarritoDetalleItem[] Items { get; set; }
        public int TotalPrecio { get; set; }
    }

    public class CarritoDetalleItem {
        public string ImagenPath { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int Precio { get; set; }
        public int Cantidad { get; set; }
    }
}
