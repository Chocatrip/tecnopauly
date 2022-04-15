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
            var datos = new Models.DatoTablaModel[10];
            for (int i = 0; i < 10; i++) {
                var fila = new Models.DatoTablaModel();
                if (i == 0)
                {
                    fila.NombreProducto = context.prueba.Select(p => p.id).FirstOrDefault().ToString();
                    fila.Categoria = context.prueba.Select(p => p.value).FirstOrDefault();
                    fila.Codigo = "Codigo " + i;
                    fila.Descripcion = "Descripcion " + i;
                    fila.PrecioCosto = i * 500;
                    fila.PrecioVenta = i * 1250;
                }
                else {
                    fila.NombreProducto = "Nombre Prueba " + i;
                    fila.Categoria = "Categoria " + i;
                    fila.Codigo = "Codigo " + i;
                    fila.Descripcion = "Descripcion " + i;
                    fila.PrecioCosto = i * 500;
                    fila.PrecioVenta = i * 1250;
                }
                

                datos[i] = fila;
            }
            

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
