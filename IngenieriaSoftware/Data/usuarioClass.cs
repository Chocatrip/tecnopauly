using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IngenieriaSoftware.Data
{
    public class usuarioClass
    {
        [Key]
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }

    }
}
