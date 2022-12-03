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
    public partial class FormEliminarP : Form
    {
        public FormEliminarP()
        {
            InitializeComponent();
        }

        private void FormEliminarP_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            lblCodigo.Visible = false;
            lblNombre.Visible = false;
            lblDescripcion.Visible = false;
            lblCantidad.Visible = false;
            lblPrecio.Visible = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            txtCodigo.Enabled = false;
            btnBuscar.Enabled = false;
            try
            {
                ConexionBD con = new ConexionBD();
                SqlCommand comando = new SqlCommand();
                comando.Connection = con.abrirConexion();
                comando.CommandText = "select codigo, nombre, descripcion, cantidad, precio from Productos where codigo = @codigo";
                comando.Parameters.AddWithValue("@codigo", txtCodigo.Text);
                SqlDataReader datos = comando.ExecuteReader();
                if (datos.Read())
                {
                    lblCodigo.Text = datos["codigo"].ToString();
                    lblNombre.Text = datos["nombre"].ToString();
                    lblDescripcion.Text = datos["descripcion"].ToString();
                    lblCantidad.Text = datos["cantidad"].ToString();
                    lblPrecio.Text = "$ " + datos["precio"].ToString();
                    lblCodigo.Visible = true;
                    lblNombre.Visible = true;
                    lblDescripcion.Visible = true;
                    lblCantidad.Visible = true;
                    lblPrecio.Visible = true;
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtCodigo.Enabled = true;
            btnBuscar.Enabled = true;
            lblCodigo.Visible = false;
            lblNombre.Visible = false;
            lblDescripcion.Visible = false;
            lblCantidad.Visible = false;
            lblPrecio.Visible = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ConexionBD con = new ConexionBD();
                SqlCommand comando = new SqlCommand();
                comando.Connection = con.abrirConexion();
                comando.CommandText = "delete Productos where codigo = @codigo";
                comando.Parameters.AddWithValue("@codigo", txtCodigo.Text);
                int RowsAfec = comando.ExecuteNonQuery();
                if (RowsAfec == 1)
                {
                    MessageBox.Show("Se Elimino el Registro del Producto", "CONFIRMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigo.Clear();
                    txtCodigo.Enabled = true;
                    btnBuscar.Enabled = true;
                }
                else
                {
                    MessageBox.Show("No se Elimino el Registro del Producto", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCodigo.Clear();
                    txtCodigo.Enabled = true;
                    btnBuscar.Enabled = true;
                }
                con.cerrarConexion();
                //Ocultar Labels
                lblCodigo.Visible = false;
                lblNombre.Visible = false;
                lblDescripcion.Visible = false;
                lblCantidad.Visible = false;
                lblPrecio.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Intentar Eliminar el Producto " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
