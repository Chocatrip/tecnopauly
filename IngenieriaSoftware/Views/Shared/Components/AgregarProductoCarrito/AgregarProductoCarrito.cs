using IngenieriaSoftware.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace IngenieriaSoftware.Views.Shared.Components.AgregarProductoCarrito
{
    public class AgregarProductoCarrito : ViewComponent
    {
        protected readonly AppDbContext context;
        public AgregarProductoCarrito(AppDbContext db)
        {
            context = db;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id, string NombreProducto, string Descripcion, string Marca, int PrecioVenta)
        {
            var model = new AgregarProductoCarritoModel {
                id = id,
                Descripcion = Descripcion,
                Marca = Marca,
                PrecioVenta = PrecioVenta,
                NombreProducto = NombreProducto
            };
            return View(model);
        }

    }

    public class AgregarProductoCarritoModel {
        public int id { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public int PrecioVenta { get; set; }
    }
}
