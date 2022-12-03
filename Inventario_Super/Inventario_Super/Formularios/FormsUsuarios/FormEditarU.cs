using Inventario_Super.Clases;
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

namespace Inventario_Super.Formularios.FormsUsuarios
{
    public partial class FormEditarU : Form, InterfaceInfoU
    {
        public FormEditarU()
        {
            InitializeComponent();
        }

        public string usuarioAutorizado;

        private void checkBoxActivo_Click(object sender, EventArgs e)
        {
            checkBoxInactivo.Checked = false;
        }

        private void checkBoxInactivo_Click(object sender, EventArgs e)
        {
            checkBoxActivo.Checked = false;
        }

        private void FormEditarU_Load(object sender, EventArgs e)
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtId.Enabled = true;
            txtNombreCompleto.Clear();
            txtUsuario.Clear();
            txtContraseña.Clear();
            comboBoxTipo.SelectedIndex = 0;
            checkBoxActivo.Checked = false;
            checkBoxInactivo.Checked = false;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                ConexionBD con = new ConexionBD();
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "update Usuarios set nombreCompleto = @nombreC, usuario = @usuario, contraseña = @contraseña, tipo = @tipo, habilitado = @habilitado, " +
                    "usuarioAutorizado = @usuarioA, fecha = @fecha where idUsuario = @id";
                comando.Parameters.AddWithValue("@nombreC",txtNombreCompleto.Text);
                comando.Parameters.AddWithValue("@usuario",txtUsuario.Text);
                comando.Parameters.AddWithValue("@contraseña",txtContraseña.Text);
                comando.Parameters.AddWithValue("@tipo",comboBoxTipo.SelectedItem.ToString());
                int habilitado;
                if (checkBoxActivo.Checked == true)
                {
                    habilitado = 1;
                }
                else
                {
                    habilitado = 0;
                }
                comando.Parameters.AddWithValue("@habilitado",habilitado);
                comando.Parameters.AddWithValue("@usuarioA",usuarioAutorizado);
                comando.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
                comando.Parameters.AddWithValue("@id", txtId.Text);
                comando.Connection = con.abrirConexion();
                int fila = comando.ExecuteNonQuery();
                if (fila == 1)
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            try
            {
                ConexionBD con = new ConexionBD();
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "select nombreCompleto,usuario,contraseña,tipo,habilitado from Usuarios where idUsuario = @id";
                comando.Parameters.AddWithValue("@id", txtId.Text);
                comando.Connection = con.abrirConexion();
                SqlDataReader datos = comando.ExecuteReader();
                if (datos.Read())
                {
                    txtNombreCompleto.Text = datos["nombreCompleto"].ToString();
                    txtUsuario.Text = datos["usuario"].ToString();
                    txtContraseña.Text = datos["contraseña"].ToString();
                    switch (datos["tipo"].ToString())
                    {
                        case "Programador":
                            comboBoxTipo.SelectedIndex = 1;
                            break;
                        case "Gerente":
                            comboBoxTipo.SelectedIndex = 2;
                            break;
                        case "Jefe":
                            comboBoxTipo.SelectedIndex = 3;
                            break;
                        case "Supervisor":
                            comboBoxTipo.SelectedIndex = 4;
                            break;
                        case "Encargado":
                            comboBoxTipo.SelectedIndex = 5;
                            break;
                        default:
                            comboBoxTipo.SelectedIndex = 0;
                            break;
                    }
                    if (datos["habilitado"].Equals(1))
                    {
                        checkBoxActivo.Checked = true;
                    }
                    else
                    {
                        checkBoxInactivo.Checked = true;
                    }
                    MessageBox.Show("Consulta exitosa con la Base de Datos", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Habilitamos y limpiamos el textBox para que consulte otro empleado
                    MessageBox.Show("No se encontro ningún Empleado con el ID: " + txtId.Text, "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtId.Clear();
                    txtId.Enabled = true;
                }
                con.cerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al realizar consulta con la Base de Datos" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ObtenerInformacionUsuario(string usuario, string tipo)
        {
            usuarioAutorizado = usuario;
        }
    }
}