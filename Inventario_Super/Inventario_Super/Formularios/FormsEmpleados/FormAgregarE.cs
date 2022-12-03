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

namespace Inventario_Super.Formularios
{
    public partial class FormAgregarE : Form, InterfaceInfoU
    {
        public string usuarioAutorizado;

        public FormAgregarE()
        {
            InitializeComponent();
        }

        private void FormAgregarE_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            //Agregar valores a el ComboBox
            comboBoxSexo.Items.Insert(0, "Seleccionar");
            comboBoxSexo.Items.Insert(1,"Masculino");
            comboBoxSexo.Items.Insert(2,"Femenino");
            //Ocultar label de fecha y hora
            lblFecha.Visible = false;
            //Valor seleccionado por defecto en el ComboBox
            comboBoxSexo.SelectedIndex = 0;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombres.Clear();
            txtApellidos.Clear();
            txtEdad.Clear();
            txtDui.Clear();
            txtNit.Clear();
            txtTelefono.Clear();
            txtCelular.Clear();
            txtDireccion.Clear();
            comboBoxSexo.SelectedIndex = 0;
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
                comando.CommandText = "insert into Empleados values (@nombres,@apellidos,@edad,@sexo,@dui,@nit,@telefono,@celular,@direccion,1,@usuarioA,@fecha)";
                comando.Parameters.AddWithValue("@nombres",txtNombres.Text);
                comando.Parameters.AddWithValue("@apellidos", txtApellidos.Text);
                comando.Parameters.AddWithValue("@edad", txtEdad.Text);
                comando.Parameters.AddWithValue("@sexo",comboBoxSexo.SelectedItem.ToString());
                comando.Parameters.AddWithValue("@dui", txtDui.Text);
                comando.Parameters.AddWithValue("@nit", txtNit.Text);
                comando.Parameters.AddWithValue("@telefono", txtTelefono.Text);
                comando.Parameters.AddWithValue("@celular", txtCelular.Text);
                comando.Parameters.AddWithValue("@direccion", txtDireccion.Text);
                comando.Parameters.AddWithValue("@usuarioA",usuarioAutorizado);
                comando.Parameters.AddWithValue("@fecha",lblFecha.Text);
                comando.Connection = con.abrirConexion();
                int fila = comando.ExecuteNonQuery();
                if (fila == 1)
                {
                    MessageBox.Show("Registro completado en la Base de Datos", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se registro el Empleado en la Base de Datos", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                con.cerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al realizar el registro\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ObtenerInformacionUsuario(string usuario, string tipo)
        {
            usuarioAutorizado = usuario;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
        }
    }
}
