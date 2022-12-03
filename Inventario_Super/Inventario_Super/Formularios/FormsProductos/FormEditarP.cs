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
    public partial class FormEditarP : Form, InterfaceInfoU
    {
        public FormEditarP()
        {
            InitializeComponent();
        }

        public string usuarioA;

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtDescrip.Clear();
            txtCantidad.Clear();
            txtPrecio.Clear();
            txtCodigoB.Clear();
            txtCodigoB.Enabled = true;
            btnBuscar.Enabled = true;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                ConexionBD con = new ConexionBD();
                SqlCommand comando = new SqlCommand();
                comando.Connection = con.abrirConexion();
                comando.CommandText = "update Productos set codigo = @codigo,nombre = @nombre,descripcion = @descripcion,cantidad  = @cantidad,precio =  @precio, usuarioAutorizado = @usuarioAutorizado, fecha = @fecha where codigo = @codigoB";
                comando.Parameters.AddWithValue("@codigo", txtCodigo.Text);
                comando.Parameters.AddWithValue("@nombre", txtNombre.Text);
                comando.Parameters.AddWithValue("@descripcion", txtDescrip.Text);
                comando.Parameters.AddWithValue("@cantidad", txtCantidad.Text);
                comando.Parameters.AddWithValue("@precio", Convert.ToDouble(txtPrecio.Text));
                comando.Parameters.AddWithValue("@codigoB", txtCodigoB.Text);
                comando.Parameters.AddWithValue("@usuarioAutorizado",usuarioA);
                comando.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
                int RowsAfec = comando.ExecuteNonQuery();
                if (RowsAfec == 1)
                {
                    MessageBox.Show("Se Actualizo la Informacion del Producto", "CONFIRMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se Actualizo la Informacion del Producto", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                con.cerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al Actualizar la Informacion del Producto " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormEditarP_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
        }

        public void ObtenerInformacionUsuario(string usuario, string tipo)
        {
            usuarioA = usuario;   
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            txtCodigoB.Enabled = false;
            btnBuscar.Enabled = false;
            try
            {
                ConexionBD con = new ConexionBD();
                SqlCommand comando = new SqlCommand();
                comando.Connection = con.abrirConexion();
                comando.CommandText = "select codigo, nombre, descripcion, cantidad, precio from Productos where codigo = @codigo";
                comando.Parameters.AddWithValue("@codigo", txtCodigoB.Text);
                SqlDataReader datos = comando.ExecuteReader();
                if (datos.Read())
                {
                    txtCodigo.Text = datos["codigo"].ToString();
                    txtNombre.Text = datos["nombre"].ToString();
                    txtDescrip.Text = datos["descripcion"].ToString();
                    txtCantidad.Text = datos["cantidad"].ToString();
                    txtPrecio.Text = datos["precio"].ToString();
                    MessageBox.Show("Consulta exitosa con la Base de Datos", "CODIGO VALIDO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontraron registros con el codigo " + txtCodigo.Text, "CODIGO INVALIDO",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                con.cerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Realizar Consulta " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
