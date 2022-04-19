using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngenieriaSoftware.Models
{
    public class LoginModel {
        public string user { get; set; }
        public string password { get; set; }
    }
}
