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

namespace Inventario_Super.Formularios.FormsUsuarios
{
    public partial class FormConsultarU : Form
    {
        public FormConsultarU()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            btnBuscar.Enabled = false;
            try
            {
                ConexionBD con = new ConexionBD();
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "select usuario,contraseña,tipo,habilitado from Usuarios where idUsuario = @id";
                comando.Parameters.AddWithValue("@id",txtId.Text);
                comando.Connection = con.abrirConexion();
                SqlDataReader datos = comando.ExecuteReader();
                if (datos.Read())
                {
                    lblUsuario.Text = datos["usuario"].ToString();
                    lblContraseña.Text = "******" +  datos["contraseña"].ToString().Substring((datos["contraseña"].ToString().Length - 4), 4);
                    lblTipo.Text = datos["tipo"].ToString();
                    if (datos["habilitado"].Equals(1))
                    {
                        lblEstado.Text = "Activo";
                    }
                    else
                    {
                        lblEstado.Text = "Inactivo";
                    }
                    //Mostrar Labels al usuario
                    lblUsuario.Visible = true;
                    lblContraseña.Visible = true;
                    lblTipo.Visible = true;
                    lblEstado.Visible = true;
                    MessageBox.Show("Consulta exitosa con la Base de Datos", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontro ningún Usuario con el ID: " + txtId.Text, "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            txtId.Clear();
            txtId.Enabled = true;
            btnBuscar.Enabled = true;
            //Ocultar Labels al usuario
            lblUsuario.Visible = false;
            lblContraseña.Visible = false;
            lblTipo.Visible = false;
            lblEstado.Visible = false;
        }

        private void FormConsultarU_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            //Ocultar Labels al usuario
            lblUsuario.Visible = false;
            lblContraseña.Visible = false;
            lblTipo.Visible = false;
            lblEstado.Visible = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
