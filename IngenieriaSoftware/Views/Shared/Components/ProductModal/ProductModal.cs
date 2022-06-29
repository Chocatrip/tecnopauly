using IngenieriaSoftware.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace IngenieriaSoftware.Views.Shared.Components.ProductModal
{
    public class ProductModal : ViewComponent
    {
        protected readonly AppDbContext context;
        public ProductModal(AppDbContext db)
        {
            context = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new Models.TablaModel();
            return View(model);
        }

    }
}
