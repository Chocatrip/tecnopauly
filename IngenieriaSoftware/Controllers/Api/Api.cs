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
        [Route("/api/eliminar-producto-carrito/{codproducto}/{idUsuario}")]
        public async Task<IActionResult> EliminarProductoCarrito(string codproducto, string idUsuario)
        {
            try
            {
                var IdUsuario = Int32.Parse(idUsuario);
                var carrito = context.carrito.Where(c => c.id_cuenta == IdUsuario && c.activo == 1).FirstOrDefault();
                var carritoCambiar = context.carrito_detalle.Where(c => c.id_carrito == carrito.id_carrito && c.cod_prod == codproducto).FirstOrDefault();
                context.carrito_detalle.Remove(carritoCambiar);
                context.SaveChanges();

                var checkCarrito = context.carrito_detalle.Where(c => c.id_carrito == carrito.id_carrito).FirstOrDefault();
                if (checkCarrito == null) {
                    var carritoIdborrar = context.carrito.Where(c => c.id_carrito == carrito.id_carrito).FirstOrDefault();
                    context.carrito.Remove(carritoIdborrar);
                    context.SaveChanges();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("concretar-venta/{idVenta}/")]
        public async Task<IActionResult> ConcretarVenta(int idVenta)
        {
            try
            {
                var ventaACambiar = context.id_ventas.Where(iv => iv.id_venta == idVenta).FirstOrDefault();
                ventaACambiar.venta_concretada = 1;
                ventaACambiar.fecha_venta = DateTime.Now;
                await context.SaveChangesAsync();

                var productosACambiarStock = context.ventas.Where(v => v.id_venta == idVenta).ToArray();
                for (int i = 0; i < productosACambiarStock.Length; i++) {
                    var productoACambiar = context.producto.Where(p => p.id == productosACambiarStock[i].id_producto).FirstOrDefault();
                    var stockComprado = context.ventas.Where(v => v.id_venta == idVenta && v.id_producto == productosACambiarStock[i].id_producto).Select(v => v.q_prod).FirstOrDefault();
                    productoACambiar.stock = productoACambiar.stock - stockComprado;
                    await context.SaveChangesAsync();
                }
                return Ok(idVenta);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Route("eliminar-producto-venta/{idproducto}/")]
        public async Task<IActionResult> EliminarProductoVenta(int idproducto)
        {
            try
            {
                var ventaIdACambiar = context.id_ventas.Where(v => v.venta_concretada == 0).FirstOrDefault();
                var ventaACambiar = context.ventas.Where(v => v.id_venta == ventaIdACambiar.id_venta && v.id_producto == idproducto).FirstOrDefault();
                context.ventas.Remove(ventaACambiar);
                context.SaveChanges();

                var checkProductos = context.ventas.Where(v => v.id_venta == ventaACambiar.id_venta).FirstOrDefault();
                if (checkProductos == null) {
                    var id_ventasEliminar = context.id_ventas.Where(iv => iv.id_venta == ventaACambiar.id_venta).FirstOrDefault();
                    context.id_ventas.Remove(id_ventasEliminar);
                    context.SaveChanges();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("importar-cotizacion/{numeroCotizacion}/")]
        public async Task<IActionResult> ImportarCotizacion(int numeroCotizacion)
        {
            try
            {
                var carritoaBuscar = context.carrito.Where(c => c.id_carrito == numeroCotizacion && c.activo == 0).FirstOrDefault();

                if (carritoaBuscar == null) {
                    CookieOptions optionsError = new CookieOptions();
                    optionsError.Expires = DateTime.Now.AddSeconds(5);
                    Response.Cookies.Append("errorImportarCotizacion", "Cotizacion-no-existe", optionsError);

                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                var productosImportar = context.carrito_detalle.Where(cd => cd.id_carrito == carritoaBuscar.id_carrito).ToArray();

                var ventaActiva = context.id_ventas.Where(iv => iv.venta_concretada == 0).FirstOrDefault();
                if (ventaActiva == null)
                {
                    var newVentaActiva = new Data.id_ventasClass
                    {
                        fecha_ingreso_venta = DateTime.Now,
                        venta_concretada = 0,
                    };
                    await context.id_ventas.AddAsync(newVentaActiva);
                    await context.SaveChangesAsync();
                }

                ventaActiva = context.id_ventas.Where(iv => iv.venta_concretada == 0).FirstOrDefault();

                var productosExistentes = context.ventas.Where(v => v.id_venta == ventaActiva.id_venta).Select(v => v.id_producto).ToArray();
                for (int i = 0; i < productosImportar.Length; i++) {
                    if (productosExistentes.Contains(productosImportar[i].id_producto))
                    {
                        var productoModificar = context.ventas.Where(v => v.id_venta == ventaActiva.id_venta && v.id_producto == productosImportar[i].id_producto).FirstOrDefault();
                        productoModificar.q_prod += productosImportar[i].q_producto;
                        await context.SaveChangesAsync();
                    }
                    else {
                        var newProductoVenta = new Data.ventasClass {
                            id_venta = ventaActiva.id_venta,
                            id_cliente = -1,
                            id_producto = productosImportar[i].id_producto,
                            nombre_prod = productosImportar[i].nombre_producto,
                            cod_prod = productosImportar[i].cod_prod,
                            q_prod = productosImportar[i].q_producto,
                            precio_inversion = productosImportar[i].precio_inversion,
                            precio_venta = productosImportar[i].precio_venta
                        };
                        await context.ventas.AddAsync(newProductoVenta);
                        await context.SaveChangesAsync();
                    }
                }
                return Ok();
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
                    cod_prod = productoCarro.codigo,
                    imagen = productoCarro.imagen,
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
        [Route("agregar-item-venta/{idProducto}/{qProducto}")]
        public async Task<IActionResult> AgregarItemVenta(int idProducto, int qProducto)
        {
            try
            {
                var query = context.id_ventas;
                var ventaActiva = query.Where(iv => iv.venta_concretada == 0).FirstOrDefault();
                if (ventaActiva == null) {
                    var newVentaActiva = new Data.id_ventasClass {
                        fecha_ingreso_venta = DateTime.Now,
                        venta_concretada = 0,
                    };
                    await context.id_ventas.AddAsync(newVentaActiva);
                    await context.SaveChangesAsync();
                }

                ventaActiva = query.Where(iv => iv.venta_concretada == 0).FirstOrDefault();
                
                var productoVenta = context.producto.Where(p => p.id == idProducto).FirstOrDefault();
                var ventaDetalle = new Data.ventasClass
                {
                    id_venta = ventaActiva.id_venta,
                    id_cliente = -1,
                    id_producto = productoVenta.id,
                    nombre_prod = productoVenta.nombre_producto,
                    cod_prod = productoVenta.codigo,
                    q_prod = qProducto,
                    precio_inversion = productoVenta.precio_costo,
                    precio_venta = productoVenta.precio_venta
                };

                var ventaProductoExistQ = context.ventas.Where(v => v.id_venta == ventaActiva.id_venta && v.id_producto == productoVenta.id).FirstOrDefault();
                
                if (ventaProductoExistQ != null) {
                    ventaProductoExistQ.q_prod += qProducto;
                    await context.SaveChangesAsync();
                    return Ok();
                }

                await context.ventas.AddAsync(ventaDetalle);
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
