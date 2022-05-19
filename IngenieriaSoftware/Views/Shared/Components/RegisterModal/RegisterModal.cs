using IngenieriaSoftware.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace IngenieriaSoftware.Views.Shared.Components.RegisterModal
{
    public class RegisterModal : ViewComponent
    {
        protected readonly AppDbContext context;
        public RegisterModal(AppDbContext db)
        {
            context = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }

    }
}
