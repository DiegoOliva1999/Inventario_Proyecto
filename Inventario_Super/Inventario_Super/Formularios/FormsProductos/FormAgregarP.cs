using Inventario_Super.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventario_Super.Formularios.FormsProductos
{
    public partial class FormAgregarP : Form, InterfaceInfoU
    {
        public FormAgregarP()
        {
            InitializeComponent();
        }

        public string UsuarioA;

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtDescrip.Clear();
            txtCantidad.Clear();
            txtPrecio.Clear();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                ConexionBD con = new ConexionBD();
                SqlCommand comando = new SqlCommand();
                comando.Connection = con.abrirConexion();
                comando.CommandText = "insert into Productos values(@codigo,@nombre,@descripcion,@cantidad,@precio,@usuarioAutorizado,@fecha)";
                comando.Parameters.AddWithValue("@codigo", txtCodigo.Text);
                comando.Parameters.AddWithValue("@nombre", txtNombre.Text);
                comando.Parameters.AddWithValue("@descripcion", txtDescrip.Text);
                comando.Parameters.AddWithValue("@cantidad", txtCantidad.Text);
                comando.Parameters.AddWithValue("@precio", txtPrecio.Text);
                comando.Parameters.AddWithValue("@usuarioAutorizado",UsuarioA);
                comando.Parameters.AddWithValue("@fecha",DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
                int RowsAfec = comando.ExecuteNonQuery();
                if (RowsAfec == 1)
                {
                    MessageBox.Show("Se Agrego el Nuevo Producto", "CONFIRMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se Agrego el Producto ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                con.cerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Insertar Datos a la Base de Datos " + ex.Message, "FALLO CON BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ObtenerInformacionUsuario(string usuario, string tipo)
        {
            UsuarioA = usuario;
        }

        private void FormAgregarP_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
        }
    }
}
