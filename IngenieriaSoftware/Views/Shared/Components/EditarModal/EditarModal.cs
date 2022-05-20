using IngenieriaSoftware.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace IngenieriaSoftware.Views.Shared.Components.EditarModal
{
    public class EditarModal : ViewComponent
    {
        protected readonly AppDbContext context;
        public EditarModal(AppDbContext db)
        {
            context = db;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id, string NombreProducto, string Categoria, string Descripcion, string Codigo, string Marca, int Stock, int PrecioCosto, int PrecioVenta)
        {
            var model = new Models.TablaModel();
            model.id = id;
            model.NombreProducto = NombreProducto;
            model.Categoria = Categoria;
            model.Descripcion = Descripcion;
            model.Codigo = Codigo;
            model.Marca = Marca;
            model.Stock = Stock;
            model.PrecioCosto = PrecioCosto;
            model.PrecioVenta = PrecioVenta;    
            return View(model);
        }
    }
}
