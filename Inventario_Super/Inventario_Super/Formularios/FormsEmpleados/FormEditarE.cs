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
    public partial class FormEditarE : Form
    {
        public FormEditarE()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Deshabilitamos el textbox id y el boton buscar
            btnBuscar.Enabled = false;
            txtId.Enabled = false;
            //Try Catch lo utilizamos para capturar errores
            try
            {
                //Instanciar la clase conexion para abrir una conexion con sql server
                ConexionBD con = new ConexionBD();
                //Instanciar la clase SqlCommand para ejecutar instrucciones en sql
                SqlCommand comando = new SqlCommand();
                //Asignamos la instruccion
                comando.CommandText = "select nombres,apellidos,edad,sexo,dui,nit,telefono,celular,direccion,habilitado from Empleados where idEmpleado = @id";
                //Rellena el parametro con el valor asigando
                comando.Parameters.AddWithValue("@id", txtId.Text);
                //Asignas la conexion a la instruccion a ejecutar
                comando.Connection = con.abrirConexion();
                //Lo utilizamos para leer los datos de la consulta
                SqlDataReader datos = comando.ExecuteReader();
                if (datos.Read())
                {
                    //Asignas los valores
                    txtNombres.Text = datos["nombres"].ToString();
                    txtApellidos.Text = datos["apellidos"].ToString();
                    txtEdad.Text = datos["edad"].ToString();
                    if (datos["sexo"].ToString().Equals("Masculino"))
                    {
                        comboBoxSexo.SelectedIndex = 1;
                    }
                    else
                    {
                        comboBoxSexo.SelectedIndex = 2;
                    }
                    txtDui.Text = datos["dui"].ToString();
                    txtNit.Text = datos["nit"].ToString();
                    txtTelefono.Text = datos["telefono"].ToString();
                    txtCelular.Text = datos["celular"].ToString();
                    txtDireccion.Text = datos["direccion"].ToString();
                    if (datos["habilitado"].Equals(1))
                    {
                        checkBoxActivo.Checked = true;
                        checkBoxInactivo.Checked = false;
                    }
                    else
                    {
                        checkBoxActivo.Checked = false;
                        checkBoxInactivo.Checked = true;
                    }
                    MessageBox.Show("Consulta exitosa con la Base de Datos", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontro ningún Empleado con el ID: " + txtId.Text, "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //Habilitamos y limpiamos el textBox para que consulte otro empleado
                    txtId.Clear();
                    txtId.Enabled = true;
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al realizar consulta con la Base de Datos" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormEditarE_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            //Agregar valores a el ComboBox
            comboBoxSexo.Items.Insert(0, "Seleccionar");
            comboBoxSexo.Items.Insert(1, "Masculino");
            comboBoxSexo.Items.Insert(2, "Femenino");
            //Valor seleccionado por defecto en el ComboBox
            comboBoxSexo.SelectedIndex = 0;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            //Limpiamos los campos
            txtNombres.Clear();
            txtApellidos.Clear();
            txtEdad.Clear();
            comboBoxSexo.SelectedIndex = 0;
            txtDui.Clear();
            txtNit.Clear();
            txtTelefono.Clear();
            txtCelular.Clear();
            txtDireccion.Clear();
            checkBoxActivo.Checked = false;
            checkBoxInactivo.Checked = false;
            //Habilitamos y limpiamos el textBox para que consulte otro empleado
            txtId.Clear();
            txtId.Enabled = true;
            btnBuscar.Enabled = true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBoxActivo_Click(object sender, EventArgs e)
        {
            checkBoxInactivo.Checked = false;
        }

        private void checkBoxInactivo_Click(object sender, EventArgs e)
        {
            checkBoxActivo.Checked = false;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                //Instanciar la clase conexion para abrir una conexion con sql server
                ConexionBD con = new ConexionBD();
                //Instanciar la clase SqlCommand para ejecutar instrucciones en sql
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "update Empleados set nombres = @nombres, apellidos = @apellidos, edad = @edad, " +
                    "sexo = @sexo, dui = @dui, nit = @nit, telefono = @telefono, celular = @celular, " +
                    "direccion = @direccion, habilitado = @habilitado where idEmpleado = @id";
                comando.Parameters.AddWithValue("@id",txtId.Text);
                comando.Parameters.AddWithValue("@nombres", txtNombres.Text);
                comando.Parameters.AddWithValue("@apellidos", txtApellidos.Text);
                comando.Parameters.AddWithValue("@edad", txtEdad.Text);
                comando.Parameters.AddWithValue("@sexo", comboBoxSexo.SelectedItem.ToString());
                comando.Parameters.AddWithValue("@dui", txtDui.Text);
                comando.Parameters.AddWithValue("@nit", txtNit.Text);
                comando.Parameters.AddWithValue("@telefono", txtTelefono.Text);
                comando.Parameters.AddWithValue("@celular", txtCelular.Text);
                comando.Parameters.AddWithValue("@direccion", txtDireccion.Text);
                int habilitado;
                if (checkBoxActivo.Checked == true)
                {
                    habilitado = 1;
                }
                else
                {
                    habilitado = 0;
                }
                comando.Parameters.AddWithValue("@habilitado", habilitado);
                comando.Connection = con.abrirConexion();
                int filas = comando.ExecuteNonQuery();
                if (filas == 1)
                {
                    MessageBox.Show("Registro actualizado en la Base de Datos", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se actualizo el registro del Empleado en la Base de Datos", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                con.cerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al actualizar el registro\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
