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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Ingresar() {
            return View();
        }
        public IActionResult Tabla() {
            var datos = context.producto.Select(p => new Models.DatoTablaModel {
                id = p.id,
                NombreProducto = p.nombre_producto,
                Categoria = p.categoria,
                Descripcion = p.descripcion,
                Codigo = p.codigo,
                PrecioCosto = p.precio_costo,
                PrecioVenta = p.precio_venta
            }).ToArray();
            

            var model = new Models.TablaModel();
            model.Datos = datos;

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
