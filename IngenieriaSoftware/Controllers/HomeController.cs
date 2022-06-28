using IngenieriaSoftware.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
