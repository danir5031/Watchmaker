using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WatchMaker.Models
{
    public class Conexion
    {
        public static SqlConnection Conectar()
        {
            // Asegúrate de que el nombre del servidor y la base de datos sean correctos
            SqlConnection cn = new SqlConnection("SERVER=localhost;DATABASE=Watchmaker;Integrated security=true");

            return cn; // Solo se devuelve la conexión, se abrirá y cerrará donde se use.
        }
    }
}