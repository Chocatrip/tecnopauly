using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using IngenieriaSoftware.Data;

namespace IngenieriaSoftware.Controllers.Api
{
    [Route("api")]
    public class Api : Controller
    {
        protected readonly AppDbContext context;
        public Api(AppDbContext db)
        {
            context = db;
        }
        [Route("eliminar-producto/{idProducto}/")]
        public async Task<IActionResult> EliminarProducto(int idProducto) {
            try
            {
                var idproductodebug = idProducto;
                var productoAEliminar = context.producto.Where(p => p.id == idProducto).FirstOrDefault();
                context.producto.Remove(productoAEliminar);
                await context.SaveChangesAsync();
                return Ok("Producto Eliminado: " + productoAEliminar.nombre_producto);
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Route("guardar-cotizacion/{idCarrito}/")]
        public async Task<IActionResult> GuardarCotizacion(int idCarrito)
        {
            try
            {
                var carritoCambiar = context.carrito.Where(car => car.id_carrito == idCarrito).FirstOrDefault();
                carritoCambiar.activo = 0;
                await context.SaveChangesAsync(); 
                return Ok(idCarrito);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
