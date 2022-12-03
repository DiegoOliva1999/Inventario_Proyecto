using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Inventario_Super.Clases;

namespace Inventario_Super.Formularios.FormsUsuarios
{
    public partial class FormAgregarU : Form , InterfaceInfoU
    {
        public FormAgregarU()
        {
            InitializeComponent();
        }

        public string usuarioAutorizado;

        public void ObtenerInformacionUsuario(string usuario, string tipo)
        {
            usuarioAutorizado = usuario;
        }

        private void FormAgregarU_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            comboBoxTipo.Items.Insert(0, "--Seleccione--");
            comboBoxTipo.Items.Insert(1, "Programador");
            comboBoxTipo.Items.Insert(2, "Gerente");
            comboBoxTipo.Items.Insert(3, "Jefe");
            comboBoxTipo.Items.Insert(4, "Supervisor");
            comboBoxTipo.Items.Insert(5, "Encargado");
            comboBoxTipo.SelectedIndex = 0;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ConexionBD con = new ConexionBD();
                SqlCommand comando = new SqlCommand();
                comando.CommandText = ("select nombres,apellidos from Empleados where idEmpleado = @id");
                comando.Parameters.AddWithValue("@id", txtId.Text);
                comando.Connection = con.abrirConexion();
                SqlDataReader datos = comando.ExecuteReader();
                if (datos.Read())
                {
                    lblNombreEmpleado.Text = datos["nombres"].ToString() + " " + datos["apellidos"].ToString();
                    MessageBox.Show("Consulta exitosa con la Base de Datos", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtId.Enabled = false;
                    btnBuscar.Enabled = false;
                }
                else
                {
                    MessageBox.Show("No se encontro ningún Empleado con el ID: " + txtId.Text, "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtId.Clear();
                    txtId.Enabled = true;
                    btnBuscar.Enabled = true;
                }
                con.cerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al realizar la consulta con la Base de Datos\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtUsuario.Clear();
            txtContraseña.Clear();
            comboBoxTipo.SelectedIndex = 0;
            txtId.Clear();
            txtId.Enabled = true;
            btnBuscar.Enabled = true;
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
                comando.CommandText = "insert into Usuarios values (@id,@nombreC,@usuario,@contraseña,@tipo,1,@usuarioA,@fecha)";
                comando.Parameters.AddWithValue("@id",txtId.Text);
                comando.Parameters.AddWithValue("@nombreC", lblNombreEmpleado.Text);
                comando.Parameters.AddWithValue("@usuario",txtUsuario.Text);
                comando.Parameters.AddWithValue("@contraseña",txtContraseña.Text);
                comando.Parameters.AddWithValue("@tipo",comboBoxTipo.SelectedItem.ToString());
                comando.Parameters.AddWithValue("@usuarioA",usuarioAutorizado);
                comando.Parameters.AddWithValue("@fecha",DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
                comando.Connection = con.abrirConexion();
                int filas = comando.ExecuteNonQuery();
                if (filas == 1)
                {
                    MessageBox.Show("Registro completado en la Base de Datos", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se registro el Usuario en la Base de Datos", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                con.cerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al realizar el registro\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
