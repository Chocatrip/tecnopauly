using IngenieriaSoftware.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;

namespace IngenieriaSoftware.Controllers
{
    public class LoginController : Controller
    {
        protected readonly AppDbContext context;
        public LoginController(AppDbContext db) {
            context = db;
        }
        [HttpPost("")]
        public IActionResult Login([FromForm] IngenieriaSoftware.Models.LoginModel model)
        {
            if (!String.IsNullOrEmpty(model.user) && !String.IsNullOrEmpty(model.password))
            {
                var query = context.cuenta.AsQueryable();
                var tipocuenta = query.Where(q => String.Equals(q.username, model.user)).FirstOrDefault().tipocuenta;
                var userdebug = model.user;
                var passdebug = model.password;
                var test = context.cuenta.Where(c => String.Equals(c.username, model.user)).Select(c => c.username).FirstOrDefault();

                if (query.Where(q => String.Equals(q.username, model.user)).Any())
                {
                    if (query.Where(q => (String.Equals(q.username, model.user) && String.Equals(q.pass, model.password))).Any())
                    {
                        CookieOptions options = new CookieOptions();
                        options.Expires = DateTime.Now.AddHours(3);
                        Response.Cookies.Append("userInfo", tipocuenta.ToString(), options);
                        
                    }
                }
            }
            return View("~/Views/Login/Index.cshtml");
        }
    }
}
