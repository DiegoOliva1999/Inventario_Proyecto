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
    public partial class FormEliminarE : Form
    {
        public FormEliminarE()
        {
            InitializeComponent();
        }

        private void FormEliminarE_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            //Ocultamos los labels para que el usuario no vea la información
            lblNombres.Visible = false;
            lblApellidos.Visible = false;
            lblEdad.Visible = false;
            lblSexo.Visible = false;
            lblDui.Visible = false;
            lblNit.Visible = false;
            lblTelefono.Visible = false;
            lblCelular.Visible = false;
            lblHabilitado.Visible = false;
            lblDireccion.Visible = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            btnBuscar.Enabled = false;
            try
            {
                ConexionBD con = new ConexionBD();
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "select nombres,apellidos,edad,sexo,dui,nit,telefono,celular,direccion,habilitado from Empleados where idEmpleado = @id";
                comando.Parameters.AddWithValue("@id", txtId.Text);
                comando.Connection = con.abrirConexion();
                SqlDataReader datos = comando.ExecuteReader();
                if (datos.Read())
                {
                    lblNombres.Text = datos["nombres"].ToString();
                    lblApellidos.Text = datos["apellidos"].ToString();
                    lblEdad.Text = datos["edad"].ToString();
                    lblSexo.Text = datos["sexo"].ToString();
                    lblDui.Text = datos["dui"].ToString();
                    lblNit.Text = datos["nit"].ToString();
                    lblTelefono.Text = datos["telefono"].ToString();
                    lblCelular.Text = datos["celular"].ToString();
                    lblDireccion.Text = datos["direccion"].ToString();
                    if (datos["habilitado"].ToString().Equals("1"))
                    {
                        lblHabilitado.Text = "Activo";
                    }
                    else
                    {
                        lblHabilitado.Text = "Inactivo";
                    }
                    //Mostrar labels al usuario
                    lblNombres.Visible = true;
                    lblApellidos.Visible = true;
                    lblEdad.Visible = true;
                    lblSexo.Visible = true;
                    lblDui.Visible = true;
                    lblNit.Visible = true;
                    lblTelefono.Visible = true;
                    lblCelular.Visible = true;
                    lblHabilitado.Visible = true;
                    lblDireccion.Visible = true;
                    MessageBox.Show("Consulta exitosa con la Base de Datos", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontro ningún Empleado con el ID: " + txtId.Text, "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //Habilitamos el boton buscar y limpiamos el textBox para que consulte otro empleado
                    txtId.Clear();
                    txtId.Enabled = true;
                    btnBuscar.Enabled = true;
                }
                con.cerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al realizar consulta con la Base de Datos\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ConexionBD con = new ConexionBD();
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "delete from Empleados where idEmpleado = @id";
                comando.Parameters.AddWithValue("id", txtId.Text);
                comando.Connection = con.abrirConexion();
                int filas = comando.ExecuteNonQuery();
                if (filas == 1)
                {
                    //Ocultamos los labels para que el usuario no vea la información
                    lblNombres.Visible = false;
                    lblApellidos.Visible = false;
                    lblEdad.Visible = false;
                    lblSexo.Visible = false;
                    lblDui.Visible = false;
                    lblNit.Visible = false;
                    lblTelefono.Visible = false;
                    lblCelular.Visible = false;
                    lblHabilitado.Visible = false;
                    lblDireccion.Visible = false;
                    MessageBox.Show("Se elimino el registro del empleado en la Base de Datos", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Habilitamos el boton buscar y limpiamos el textBox para que consulte otro empleado
                    txtId.Clear();
                    txtId.Enabled = true;
                    btnBuscar.Enabled = true;
                }
                else
                {
                    //Habilitamos el boton buscar y limpiamos el textBox para que consulte otro empleado
                    txtId.Clear();
                    txtId.Enabled = true;
                    btnBuscar.Enabled = true;
                    MessageBox.Show("No se elimino el registro del empleado en la Base de Datos", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                con.cerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al intentar eliminar el registro en la Base de Datos\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            btnBuscar.Enabled = true;
            //Ocultamos los labels para que el usuario no vea la información
            lblNombres.Visible = false;
            lblApellidos.Visible = false;
            lblEdad.Visible = false;
            lblSexo.Visible = false;
            lblDui.Visible = false;
            lblNit.Visible = false;
            lblTelefono.Visible = false;
            lblCelular.Visible = false;
            lblHabilitado.Visible = false;
            lblDireccion.Visible = false;
            //Habilitamos y limpiamos el textBox para que consulte otro empleado
            txtId.Clear();
            txtId.Enabled = true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
