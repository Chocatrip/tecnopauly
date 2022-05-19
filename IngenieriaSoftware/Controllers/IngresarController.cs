using IngenieriaSoftware.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace IngenieriaSoftware.Controllers
{
    public class IngresarController : Controller
    {
        protected readonly AppDbContext context;
        public IngresarController(AppDbContext db) {
            context = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("ingresar-producto")]
        public async Task<ActionResult> IngresarProducto([FromForm] IngenieriaSoftware.Models.DatoTablaModel model) {
            var newProducto = new IngenieriaSoftware.Data.productoClass() {
                id = model.id,
                nombre_producto = model.NombreProducto,
                categoria = model.Categoria,
                descripcion = model.Descripcion,
                marca = model.Marca,
                stock = model.Stock,
                codigo = model.Codigo,
                precio_costo = model.PrecioCosto,
                precio_venta = model.PrecioVenta
            };

            using (var ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {
                await context.producto.AddAsync(newProducto);
                await context.SaveChangesAsync();
                ts.Complete();
                ts.Dispose();
            }
                
            return Ok(model);
        }
    }
}
