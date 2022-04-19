using IngenieriaSoftware.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngenieriaSoftware.Controllers
{
    public class LoginController : Controller
    {
        protected readonly AppDbContext context;
        Blazored.SessionStorage.ISessionStorageService sessionStorage;
        public LoginController(AppDbContext db) {
            context = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("Login")]
        public async Task<ActionResult> LoginControl([FromForm] IngenieriaSoftware.Models.LoginModel model)
        {
            if (!String.IsNullOrEmpty(model.user) && !String.IsNullOrEmpty(model.password))
            {
                var query = context.cuenta.AsQueryable();
                var tipocuenta = 1;
                var userdebug = model.user;
                var passdebug = model.password;
                var test = context.cuenta.Where(c => String.Equals(c.username, model.user)).Select(c => c.username).FirstOrDefault();

                if (query.Where(q => String.Equals(q.username, model.user)).Any())
                {
                    if (query.Where(q => (String.Equals(q.username, model.user) && String.Equals(q.pass, model.password))).Any())
                    {

                        var userObject = query
                            .Where(q => String.Equals(q.username, model.user))
                            .Select(q => new
                            {
                                username = q.username,
                                pass = q.pass,
                                tipocuenta = q.tipocuenta
                            }).FirstOrDefault();

                        await sessionStorage.SetItemAsync("Username", userObject.username);
                        await sessionStorage.SetItemAsync("TipoCuenta", userObject.tipocuenta.ToString());
                    }
                }
            }
            return View();
        }
    }
}
