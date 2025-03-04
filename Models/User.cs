using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WatchMaker.Models
{
    public class User
    {
        public int Id_user { get; set; }
        public string Usuario { get; set; }
        public string Pass { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string rol { get; set; }
    }
}