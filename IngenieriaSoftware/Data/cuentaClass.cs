using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IngenieriaSoftware.Data
{
    public class cuentaClass
    {
        [Key]
        [Column("id")]
        public int id { get; set; }
        public int id_usuario { get; set; }
        public string username { get; set; }
        public string pass { get; set; }
        public int tipocuenta { get; set; }
    }
}
