using IngenieriaSoftware.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace IngenieriaSoftware.Views.Shared.Components.AgregarProductoVenta
{
    public class AgregarProductoVenta : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int IdProducto, string NombreProducto, string CodigoProducto, int Stock,string Marca, int PrecioVenta)
        {
            var model = new AgregarProductoVentaModel
            {
                IdProducto = IdProducto,
                NombreProducto = NombreProducto,
                CodigoProducto = CodigoProducto,
                Stock = Stock,
                Marca = Marca,
                PrecioVenta = PrecioVenta
            };
            return View(model);
        }
    }

    public class AgregarProductoVentaModel {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string CodigoProducto { get; set; }
        public string Marca { get; set; }
        public int Stock { get; set; }
        public int PrecioVenta { get; set; }
    }
}
