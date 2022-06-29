using IngenieriaSoftware.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using IngenieriaSoftware.Data;

namespace IngenieriaSoftware.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        protected readonly AppDbContext context;
        public HomeController(ILogger<HomeController> logger, AppDbContext db)
        {
            _logger = logger;
            context = db;
        }

        public IActionResult Index()
        {

            var session = (Request.Cookies["userInfo"] ?? "").ToString();

            if (!String.IsNullOrEmpty(session))
            {
                ViewBag.Session = Int32.Parse(session);
            }
            else {
                ViewBag.Session = 3;
            }
            var datos = context.producto.Select(p => new Models.DatoTablaModel
            {
                id = p.id,
                NombreProducto = p.nombre_producto,
                Categoria = p.categoria,
                Descripcion = p.descripcion,
                Codigo = p.codigo,
                Marca = p.marca,
                Stock = p.stock,
                PrecioCosto = p.precio_costo,
                PrecioVenta = p.precio_venta
            }).ToArray();
            var model = new Models.TablaModel();
            model.Datos = datos;
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login() {
            return View();
        }
        public IActionResult Ingresar() {
            return View();
        }
        public IActionResult Registrar() {
            return View();
        }
        public IActionResult Productos()
        {
            var session = (Request.Cookies["userInfo"] ?? "").ToString();

            if (!String.IsNullOrEmpty(session))
            {
                ViewBag.Session = Int32.Parse(session);
            }
            else
            {
                ViewBag.Session = 3;
            }
            var datos = context.producto.Select(p => new Models.DatoTablaModel
            {
                id = p.id,
                NombreProducto = p.nombre_producto,
                Categoria = p.categoria,
                Descripcion = p.descripcion,
                Codigo = p.codigo,
                Marca = p.marca,
                Stock = p.stock,
                PrecioCosto = p.precio_costo,
                PrecioVenta = p.precio_venta
            }).ToArray();
            var model = new Models.TablaModel();
            model.Datos = datos;
            return View(model);
        }

        public IActionResult Tabla() {
            var datos = context.producto.Select(p => new Models.DatoTablaModel {
                id = p.id,
                NombreProducto = p.nombre_producto,
                Categoria = p.categoria,
                Descripcion = p.descripcion,
                Codigo = p.codigo,
                Marca = p.marca,
                Stock = p.stock,
                PrecioCosto = p.precio_costo,
                PrecioVenta = p.precio_venta
            }).ToArray();


            var model = new Models.TablaModel();
            model.Datos = datos;

            return View(model);
        }
        public class HistorialdeVentasModelWrapper {
            public HistorialdeVentasModel[] ventasHistorial { get; set; }
        }
        public class HistorialdeVentasModel  {
            public int idVenta { get; set; }
            public int Total { get; set; }
            public DateTime fechaIngreso { get; set; }
            public DateTime fechaVenta { get; set; }
            public string productos { get; set; }
        }
        public IActionResult HistorialdeVentas()
        {
            var model = new HistorialdeVentasModelWrapper();
            var ventas = context.id_ventas
                .Where(v => v.venta_concretada == 1).ToArray();

            HistorialdeVentasModel[] ventasMostrar = new HistorialdeVentasModel[ventas.Length];


            for (int i = 0; i < ventasMostrar.Length; i++) {
                var newItem = new HistorialdeVentasModel();
                var itemenTabla = context.ventas.Where(v => v.id_venta == ventas[i].id_venta).ToArray();
                newItem.idVenta = ventas[i].id_venta;
                string productos = "";
                var total = 0;
                for (int j = 0; j < itemenTabla.Length; j++) {
                    total = total + (itemenTabla[j].precio_venta * itemenTabla[j].q_prod);
                    productos = productos + itemenTabla[j].nombre_prod +" x " +itemenTabla[j].q_prod.ToString() + ", ";
                }
                newItem.Total = total;
                newItem.productos = productos;
                newItem.fechaIngreso = ventas[i].fecha_ingreso_venta;
                newItem.fechaVenta = ventas[i].fecha_venta;
                
                ventasMostrar[i] = newItem;
            }

            model.ventasHistorial = ventasMostrar;
            return View(model);
        }
        public class CotizacionesModelWrapper {
            public CotizacionesModel[] itemsCotizaciones { get; set; }
        }
        public class CotizacionesModel
        {
            public int idCuenta { get; set; }
            public int idCarrito { get; set; }
            public string Productos { get; set; }
            public int Total { get; set; }
            public string cActiva { get; set; }
        }
        public IActionResult Cotizaciones()
        {
            var model = new CotizacionesModelWrapper(); ;

            var carritos = context.carrito.ToArray();
            CotizacionesModel[] itemsenTabla = new CotizacionesModel[carritos.Length];
            
            var todosLosDatos = context.carrito_detalle.ToArray();
            for (int i = 0; i < carritos.Length; i++) {

                var idCuenta = carritos[i].id_cuenta;
                var idCarrito = carritos[i].id_carrito;
                var datosllamar = context.carrito_detalle.Where(cd=> cd.id_carrito == idCarrito).ToArray();

                var Productos = "";
                var Total = 0;
                var cActiva = carritos[i].activo;
                for (int j = 0; j < datosllamar.Length; j++) {
                    Total = Total + (datosllamar[j].precio_venta*datosllamar[j].q_producto);
                    Productos = Productos + datosllamar[j].nombre_producto + " x " + datosllamar[j].q_producto.ToString() + ", ";
                }
                var cActivaString = "";
                if (cActiva == 0) {
                    cActivaString = "Cotizada";
                }else
                {
                    cActivaString = "En proceso de cotizacion";
                }
                var data = new CotizacionesModel
                {
                    idCuenta = idCuenta,
                    idCarrito = idCarrito,
                    Productos = Productos,
                    Total = Total,
                    cActiva = cActivaString
                };
                itemsenTabla[i] = data;
            }
            model.itemsCotizaciones = itemsenTabla;
            return View(model);
        }
        public async Task<ActionResult> EditarProducto([FromForm] IngenieriaSoftware.Models.DatoTablaModel model) {

            var productoACambiar = context.producto.Where(p => p.id == model.id).FirstOrDefault();
            productoACambiar.nombre_producto = model.NombreProducto;
            productoACambiar.descripcion = model.Descripcion;
            productoACambiar.stock = model.Stock;
            productoACambiar.categoria = model.Categoria;
            productoACambiar.marca = model.Marca;
            productoACambiar.precio_costo = model.PrecioCosto;
            productoACambiar.precio_venta = model.PrecioVenta;

            await context.SaveChangesAsync();
            return Ok();
        }

        public IActionResult Contacto() {
            var session = (Request.Cookies["userInfo"] ?? "").ToString();

            if (!String.IsNullOrEmpty(session))
            {
                ViewBag.Session = Int32.Parse(session);
            }
            else
            {
                ViewBag.Session = 3;
            }

            return View();
        }

        public IActionResult Cuenta()
        {
            var session = (Request.Cookies["userInfo"] ?? "").ToString();
            var IdUsuarioString = Request.Cookies["userId"].ToString();
            var IdUsuario = Int32.Parse(IdUsuarioString);
            if (!String.IsNullOrEmpty(session))
            {
                ViewBag.Session = Int32.Parse(session);
            }
            else
            {
                ViewBag.Session = 3;
            }

            var model = new Models.EditarCuentaModel();

            var userQuery = context.usuario.Where(u => u.id_usuario == IdUsuario).FirstOrDefault();
            var cuentaQuery = context.cuenta.Where(c => c.id_usuario == IdUsuario).FirstOrDefault();

            model.IdUsuario = IdUsuario;
            model.Nombre = userQuery.nombre;
            model.Email = userQuery.correo;
            model.Username = cuentaQuery.username;
            model.CurrentPass = "";
            model.NewPass = "";
            return View(model);
        }
        public async Task<ActionResult> EditarCuenta([FromForm] IngenieriaSoftware.Models.EditarCuentaModel model)
        {
            if (String.IsNullOrEmpty(model.Nombre) || String.IsNullOrEmpty(model.Username) || String.IsNullOrEmpty(model.Email) || String.IsNullOrEmpty(model.CurrentPass) || String.IsNullOrEmpty(model.NewPass)) {
                CookieOptions optionsError = new CookieOptions();
                optionsError.Expires = DateTime.Now.AddSeconds(3);
                Response.Cookies.Append("errorEditarCuenta", "No-se-permiten-formularios-en-blanco.", optionsError);
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var idUsuario = model.IdUsuario;
            var nombre = model.Nombre.Trim().ToLower();
            var correo = model.Email.Trim().ToLower();
            var username = model.Username.Trim().ToLower();
            var currentPass = model.CurrentPass.Trim().ToLower();
            var newPass = model.NewPass.Trim().ToLower();

            var queryCuenta = context.cuenta;
            var queryUsuario = context.usuario;
            if (queryCuenta.Where(c => String.Equals(c.username, username) && c.id_usuario!=idUsuario).Any())
            {
                CookieOptions optionsError = new CookieOptions();
                optionsError.Expires = DateTime.Now.AddSeconds(5);
                Response.Cookies.Append("errorEditarCuenta", "Ya-existe-un-usuario-con-ese-nombre-de-usuario.", optionsError);
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            if (!String.Equals(queryCuenta.Where(c => c.id_usuario == idUsuario).Select(c => c.pass).FirstOrDefault(), currentPass)) {
                CookieOptions optionsError = new CookieOptions();
                optionsError.Expires = DateTime.Now.AddSeconds(5);
                Response.Cookies.Append("errorEditarCuenta", "Contrasena-actual-no-corresponde-a-la-ingresada.", optionsError);
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            if (newPass.Length < 8 || newPass.Length > 20)
            {
                CookieOptions optionsError = new CookieOptions();
                optionsError.Expires = DateTime.Now.AddSeconds(5);
                Response.Cookies.Append("errorEditarCuenta", "Ingrese-una-contrasena-entre-8-y-20-caracteres.", optionsError);
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var usuarioACambiar = context.usuario.Where(u => u.id_usuario == model.IdUsuario).FirstOrDefault();
            var cuentaACambiar = context.cuenta.Where(c => c.id_usuario == model.IdUsuario).FirstOrDefault();

            usuarioACambiar.nombre = nombre;
            usuarioACambiar.correo = correo;
            cuentaACambiar.username = username;
            cuentaACambiar.pass = newPass;
            await context.SaveChangesAsync();
            return Ok();
        }


        public IActionResult CarritoCompra()
        {
            var idUsuarioString = (Request.Cookies["userId"] ?? "").ToString();
            var idUsuario = Int32.Parse(idUsuarioString);

            var carritoActivo = context.carrito.Where(car => car.id_cuenta == idUsuario && car.activo == 1).FirstOrDefault();

            if (carritoActivo == null) {
                var emptyModel = new Models.CarritoDetalleModel();
                return View(emptyModel);
            }
            var itemsDetalle = context.carrito_detalle.Where(cd => cd.id_carrito == carritoActivo.id_carrito).ToArray();

            var model = new Models.CarritoDetalleModel();

            var queryProducto = context.producto;
            var totalPrecio = 0;
            Models.CarritoDetalleItem[] itemsArray = new CarritoDetalleItem[itemsDetalle.Length];

            for (int i = 0; i < itemsDetalle.Length; i++) {
                var item = new Models.CarritoDetalleItem {
                    Nombre = itemsDetalle[i].nombre_producto,
                    Codigo = itemsDetalle[i].cod_prod,
                    Cantidad = itemsDetalle[i].q_producto,
                    Precio = itemsDetalle[i].precio_venta,
                    ImagenPath = itemsDetalle[i].imagen
                };

                totalPrecio += itemsDetalle[i].q_producto * itemsDetalle[i].precio_venta;
                itemsArray[i] = item;
            }
            model.IdUsuario = idUsuario;
            model.IdCarrito = carritoActivo.id_carrito;
            model.Items = itemsArray;
            model.TotalPrecio = totalPrecio;
            return View(model);
        }

        public IActionResult Venta() {
            var ventaActiva = context.id_ventas.Where(iv => iv.venta_concretada == 0).FirstOrDefault();

            if (ventaActiva == null) {
                var emptyModel = new Models.VentaDetalleModel();
                return View(emptyModel);
            }
            var itemsDetalle = context.ventas.Where(v => v.id_venta == ventaActiva.id_venta).ToArray();

            var model = new Models.VentaDetalleModel();

            Models.VentaDetalleItem[] itemsArray = new VentaDetalleItem[itemsDetalle.Length]; 
            var total_precio = 0;
            var total_invertido = 0;

            
            for (int i = 0; i < itemsDetalle.Length; i++) {
                var item = new Models.VentaDetalleItem
                {
                    id_producto = itemsDetalle[i].id_producto,
                    nombre_prod = itemsDetalle[i].nombre_prod,
                    cod_prod = itemsDetalle[i].cod_prod,
                    q_prod = itemsDetalle[i].q_prod,
                    stockDisponible = context.producto.Where(p => p.id == itemsDetalle[i].id_producto).Select(p => p.stock).FirstOrDefault(),
                    precio_inversion = itemsDetalle[i].precio_inversion,
                    precio_venta = itemsDetalle[i].precio_venta
                };

                int checkVenta = 1;
                if (item.stockDisponible < item.q_prod) {
                    checkVenta = 0;
                }
                item.checkVenta = checkVenta;
                total_precio += itemsDetalle[i].q_prod * itemsDetalle[i].precio_venta;
                total_invertido += itemsDetalle[i].q_prod * itemsDetalle[i].precio_inversion;
                itemsArray[i] = item;

                
            }
            model.id_venta = ventaActiva.id_venta;
            model.id_cliente = itemsDetalle[0].id_cliente;
            model.fecha_ingreso_venta = ventaActiva.fecha_ingreso_venta;
            model.total_precio = total_precio;
            model.total_ganancia = total_precio - total_invertido;

            model.itemsVenta = itemsArray;

            return View(model);
        }
        

        public IActionResult PruebaGuardarCotizacion() {
            var cotizacion = context.carrito.Where(car=> car.id_cuenta==1).FirstOrDefault();
            return Ok(cotizacion);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
