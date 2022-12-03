using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario_Super.Clases
{
    class ConexionBD
    {
        //string cadena = "Data Source = DESKTOP-D8N23G7\\SQLEXPRESS; Initial Catalog = Inventario; Integrated Security = True";
        string cadena2 = "Data Source = DESKTOP-D8N23G7\\SQLEXPRESS; Initial Catalog = Inventario;User ID=sa;Password=alianza1999";
        private SqlConnection conectar = new SqlConnection();

        public ConexionBD()
        {
            conectar.ConnectionString = cadena2;
        }

        public SqlConnection abrirConexion()
        {
            conectar.Open();
            return conectar;
        }

        public SqlConnection cerrarConexion()
        {
            conectar.Close();
            return conectar;
        }
    }
}
