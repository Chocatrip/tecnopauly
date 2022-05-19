using IngenieriaSoftware.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Mvc.Ajax;

namespace IngenieriaSoftware.Controllers
{
    public class LoginController : Controller
    {
        protected readonly AppDbContext context;
        public LoginController(AppDbContext db) {
            context = db;
        }
        [HttpPost("")]
        public async Task<ActionResult>Login([FromForm] IngenieriaSoftware.Models.LoginModel model)
        {
            if (!String.IsNullOrEmpty(model.user) && !String.IsNullOrEmpty(model.password))
            {
                var query = context.cuenta.AsQueryable();             
                var userdebug = model.user;
                var passdebug = model.password;

                if (query.Where(q => String.Equals(q.username, model.user)).Any())
                {
                    if (query.Where(q => (String.Equals(q.username, model.user) && String.Equals(q.pass, model.password))).Any())
                    {
                        var tipocuenta = query.Where(q => String.Equals(q.username, model.user)).FirstOrDefault().tipocuenta;
                        CookieOptions options = new CookieOptions();
                        options.Expires = DateTime.Now.AddHours(3);
                        Response.Cookies.Append("userInfo", tipocuenta.ToString(), options);

                    }
                    else
                    {
                        CookieOptions optionsError = new CookieOptions();
                        optionsError.Expires = DateTime.Now.AddSeconds(2);
                        Response.Cookies.Append("errorLogin", "Contrasena-Invalida", optionsError);

                        return StatusCode(StatusCodes.Status400BadRequest);
                    }
                }
                else {
                    CookieOptions optionsError = new CookieOptions();
                    optionsError.Expires = DateTime.Now.AddSeconds(2);
                    Response.Cookies.Append("errorLogin", "Usuario-no-existe.-Registre-una-cuenta.", optionsError);

                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            else {
                //HttpContext.Response.StatusCode = 400;
                //HttpContext.Response.ContentType = "application/json";
                //await HttpContext.Response.WriteAsJsonAsync("{\"message\": Utilice credenciales validas}");
                //return null;
                CookieOptions optionsError = new CookieOptions();
                optionsError.Expires = DateTime.Now.AddSeconds(2);
                Response.Cookies.Append("errorLogin", "Ingrese-Credenciales-validas", optionsError);

                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return Ok("Inicio de sesion correcto");
        }
    }
}
