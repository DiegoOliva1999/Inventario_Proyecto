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

namespace Inventario_Super.Formularios.EntradaSalidaProductos
{
    public partial class FormEntrada : Form, InterfaceInfoU
    {
        public FormEntrada()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormEntrada_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            lblCodigo.Visible = false;
            lblNombre.Visible = false;
            txtCantidad.Enabled = false;
            lblUsuarioBD.Visible = false;
            lblFecha.Visible = false;
            lblHora.Visible = false;
            btnRegistrar.Enabled = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            txtCodigo.Enabled = false;
            btnBuscar.Enabled = false;
            txtCantidad.Enabled = true;
            btnRegistrar.Enabled = true;
            try
            {
                ConexionBD con = new ConexionBD();
                SqlCommand comando = new SqlCommand();
                comando.Connection = con.abrirConexion();
                comando.CommandText = "select codigo, nombre from Productos where codigo = @codigo";
                comando.Parameters.AddWithValue("@codigo", txtCodigo.Text);
                SqlDataReader datos = comando.ExecuteReader();
                if (datos.Read())
                {
                    lblCodigo.Text = datos["codigo"].ToString();
                    lblNombre.Text = datos["nombre"].ToString();
                    lblCodigo.Visible = true;
                    lblNombre.Visible = true;
                    txtCantidad.Enabled = true;
                    lblUsuarioBD.Visible = true;
                    lblFecha.Visible = true;
                    lblHora.Visible = true;
                    MessageBox.Show("Consulta exitosa con la Base de Datos", "CODIGO VALIDO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontraron registros con el codigo " + txtCodigo.Text, "CODIGO INVALIDO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblCodigo.Visible = false;
                    lblNombre.Visible = false;
                    txtCantidad.Enabled = false;
                    lblUsuarioBD.Visible = false;
                    lblFecha.Visible = false;
                    lblHora.Visible = false;
                    btnRegistrar.Enabled = false;
                    txtCodigo.Clear();
                    txtCodigo.Enabled = true;
                    btnBuscar.Enabled = true;
                }
                con.cerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar consulta " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToShortDateString();
            lblHora.Text = DateTime.Now.ToShortTimeString();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtCodigo.Enabled = true;
            btnBuscar.Enabled = true;
            lblCodigo.Visible = false;
            lblNombre.Visible = false;
            txtCantidad.Enabled = false;
            lblUsuarioBD.Visible = false;
            lblFecha.Visible = false;
            lblHora.Visible = false;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {          
            try
            {
                ConexionBD con = new ConexionBD();
                SqlCommand comando = new SqlCommand();
                comando.Connection = con.abrirConexion();
                comando.CommandText = "insert into EntradaProductos values(@codigo,@nombre,@cantidad,@usuario,@fecha)";
                comando.Parameters.AddWithValue("@codigo", lblCodigo.Text);
                comando.Parameters.AddWithValue("@nombre", lblNombre.Text);
                comando.Parameters.AddWithValue("@cantidad", txtCantidad.Text);
                comando.Parameters.AddWithValue("@usuario", lblUsuarioBD.Text);
                comando.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                /*-----------------------------------------------------------------------------------*/
                ConexionBD con1 = new ConexionBD();
                SqlCommand comando1 = new SqlCommand();
                comando1.Connection = con1.abrirConexion();
                comando1.CommandText = "update Productos set cantidad = cantidad + @canSumar where codigo = @codigo";
                comando1.Parameters.AddWithValue("@canSumar", txtCantidad.Text);
                comando1.Parameters.AddWithValue("@codigo", lblCodigo.Text);
                int RowsAfec = comando.ExecuteNonQuery() + comando1.ExecuteNonQuery();
                if (RowsAfec == 2)
                {
                    MessageBox.Show("Se agrego el registro", "REGISTRO EXITOSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigo.Clear();
                    txtCodigo.Enabled = true;
                    btnBuscar.Enabled = true;
                }
                else
                {
                    MessageBox.Show("No se agrego el registro ","REGISTRO INVALIDO",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCodigo.Clear();
                    txtCodigo.Enabled = true;
                    btnBuscar.Enabled = true;
                }
                con.cerrarConexion();
                con1.cerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se logro agregar el registro " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ObtenerInformacionUsuario(string usuario, string tipo)
        {
            lblUsuarioBD.Text = usuario;
        }
    }
}