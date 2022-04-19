using IngenieriaSoftware.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace IngenieriaSoftware.Controllers
{
    public class RegisterController : Controller
    {
        protected readonly AppDbContext context;
        public RegisterController(AppDbContext db) {
            context = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterControl([FromForm] IngenieriaSoftware.Models.RegistrarModel model) {
            if (String.IsNullOrEmpty(model.nombre) || String.IsNullOrEmpty(model.correo) || String.IsNullOrEmpty(model.username) || String.IsNullOrEmpty(model.pass)) {
                return StatusCode(StatusCodes.Status400BadRequest, new { Message = "No se permiten formularios en blanco." });
            }
            var nombre = model.nombre.Trim().ToLower();
            var correo = model.correo.Trim().ToLower();
            var username = model.username.Trim().ToLower();
            var pass = model.pass.Trim().ToLower();

            if (pass.Length <= 8 || pass.Length >= 20) {
                return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Ingrese una contraseña entre 8 y 20 caracteres." });
            }
            if (context.cuenta.Where(c => String.Equals(c.username, username)).Any()) {
                return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Ya existe un usuario con ese nombre." });
            }
            using (var ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {
                var newUsuario = new Data.usuarioClass
                {
                    nombre = nombre,
                    correo = correo
                };

                await context.usuario.AddAsync(newUsuario);
                await context.SaveChangesAsync();

                var idUsuario = context.usuario.Where(u => u.nombre == nombre).Select(u => u.id_usuario).FirstOrDefault();

                var newCuenta = new Data.cuentaClass
                {
                    id_usuario = idUsuario,
                    username = username,
                    pass = pass,
                    tipocuenta = 1
                };
                await context.cuenta.AddAsync(newCuenta);
                await context.SaveChangesAsync();
                ts.Complete();
                ts.Dispose();
            }
            var nuevoUsuario = new
            {
                nombre = nombre,
                correo = correo,
                username = username,
                pass = pass,
                tipocuenta = 1
            };

            return Ok(nuevoUsuario);
        }
    }
}
