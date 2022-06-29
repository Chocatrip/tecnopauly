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
        [Route("agregar-item-carrito/{idProducto}/{qProducto}")]
        public async Task<IActionResult> AgregarItemCarrito(int idProducto, int qProducto)
        {
            try
            {
                var idUsuarioString = (Request.Cookies["userId"] ?? "").ToString();
                var idUsuario = Int32.Parse(idUsuarioString);

                var query = context.carrito;

                var carritoExist = query.Any(car => car.id_cuenta == idUsuario);
                if (!carritoExist) {
                    var newCarritoActivo = new Data.carritoClass
                    {
                        id_cuenta = idUsuario,
                        activo = 1
                    };
                    await context.carrito.AddAsync(newCarritoActivo);
                    await context.SaveChangesAsync();
                }

                var carritoActivo = query.Where(car => car.id_cuenta == idUsuario && car.activo == 1).FirstOrDefault();

                if (carritoActivo == null) {
                    var newCarritoActivo = new Data.carritoClass
                    {
                        id_cuenta = idUsuario,
                        activo = 1
                    };
                    await context.carrito.AddAsync(newCarritoActivo);
                    await context.SaveChangesAsync();
                }
                carritoActivo = query.Where(car => car.id_cuenta == idUsuario && car.activo == 1).FirstOrDefault();

                var productoCarro = context.producto.Where(p => p.id == idProducto).FirstOrDefault();
                var carritoDetalle = new Data.carrito_detalleClass
                {
                    id_carrito = carritoActivo.id_carrito,
                    id_producto = productoCarro.id,
                    nombre_producto = productoCarro.nombre_producto,
                    precio_inversion = productoCarro.precio_costo,
                    precio_venta = productoCarro.precio_venta,
                    q_producto = qProducto,
                    vendido = 0
                };
                var carritoProductoExistQ = context.carrito_detalle
                    .Where(cd => cd.id_carrito == carritoActivo.id_carrito && cd.id_producto == productoCarro.id).FirstOrDefault();

                if (carritoProductoExistQ != null) {
                    carritoProductoExistQ.q_producto += qProducto;
                    await context.SaveChangesAsync();
                    return Ok();
                }

                await context.carrito_detalle.AddAsync(carritoDetalle);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
